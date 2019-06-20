using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpcRcw.Da;
using OpcRcw.Comn;
using System.Runtime.InteropServices;

namespace OPCServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //数据定义
        OpcRcw.Da.IOPCServer ServerObj; //定义OPCServer对象
        OpcRcw.Da.IOPCSyncIO IOPCSyncIO20Obj = null;    //同步读对象
        OpcRcw.Da.IOPCGroupStateMgt IOPCGroupStateMgtObj = null;    //管理OPCGroup组对象

        internal const int LOCALE_ID = 0x47;    //OPCServer语言码-英语

        Object MyobjGroup1 = null;  //OPCGroup对象
        int[] ItemServerHandle; //Item句柄数组
        int pSvrGroupHandle = 0;    //OPCGroup句柄

        //连接OPCServer，建立相应OPCGroup组，并添加需要读写的Item
        private void Btn_Conn_Click(object sender, EventArgs e)
        {
            //定义变量
            Type svrComponenttype;
            Int32 dwRequestedUpdateRate = 1000; //订阅读取速度
            Int32 hClientGroup = 1;
            Int32 pRevUpdateRate;
            OpcRcw.Da.OPCITEMDEF[] ItemArray;

            float deadband = 0;

            int TimeBias = 0;

            //使用C#托管代码时,内存地址和GC回收不需要关心,CLR已经给我们暗箱操作
            //如果在C#中调用了非托管代码,比如VC的DLL,而且它有一个回调函数,需要引用C#中的某个对象并操作,这时候需要小心
            //要是非托管代码中用到的托管代码那个对象被GC给回收了,这时候就会报内存错误
            //所以就要把那个对象钉住"Pin",让它的内存地址固定,而不被垃圾回收掉,然后我们自己管理,自己释放内存,这时候就需要GCHandle
            GCHandle hTimeBias, hDeadband;
            hTimeBias = GCHandle.Alloc(TimeBias, GCHandleType.Pinned);
            hDeadband = GCHandle.Alloc(deadband, GCHandleType.Pinned);
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            try
            {
                svrComponenttype = Type.GetTypeFromProgID("OPC.SimaticNet", "192.168.0.102");   //OPCServer
                ServerObj = (OpcRcw.Da.IOPCServer)Activator.CreateInstance(svrComponenttype);   //注册
                try
                {
                    ServerObj.AddGroup("MyOPCGroup1",   //增加组
                        0,
                        dwRequestedUpdateRate,
                        hClientGroup,
                        hTimeBias.AddrOfPinnedObject(),
                        hDeadband.AddrOfPinnedObject(),
                        LOCALE_ID,
                        out pSvrGroupHandle,
                        out pRevUpdateRate,
                        ref iidRequiredInterface,
                        out MyobjGroup1);
                    IOPCSyncIO20Obj = (IOPCSyncIO)MyobjGroup1;  //Query interface for sync calls on group object
                    IOPCGroupStateMgtObj = (IOPCGroupStateMgt)MyobjGroup1;
                    ItemArray = new OPCITEMDEF[2];  //定义读写的item,共2个变量

                    ItemArray[0].szAccessPath = "";
                    ItemArray[0].szItemID = "S7:[S7 conncetion_1]DB10,INT0";    //地址,不同数据类型表示方法不同
                    ItemArray[0].bActive = 1;   //是否激活
                    ItemArray[0].hClient = 1;   //表示ID
                    ItemArray[0].dwBlobSize = 0;
                    ItemArray[0].vtRequestedDataType = 2;
                    ItemArray[1].szAccessPath = "";
                    ItemArray[1].szItemID = "S7:[S7 connection_1]DB10,STRING14.10";
                    ItemArray[1].bActive = 1;
                    ItemArray[1].hClient = 2;
                    ItemArray[1].dwBlobSize = 0;
                    ItemArray[1].pBlob = IntPtr.Zero;
                    ItemArray[1].vtRequestedDataType = 8;
                    IntPtr pResults = IntPtr.Zero;
                    IntPtr pErrors = IntPtr.Zero;
                    try
                    {
                        ((OpcRcw.Da.IOPCItemMgt)MyobjGroup1).AddItems(2, ItemArray, out pResults, out pErrors);
                        int[] errors = new int[2];
                        IntPtr pos = pResults;
                        ItemServerHandle = new int[2];
                        Marshal.Copy(pErrors, errors, 0, 2);
                        if (errors[0] == 0)
                        {
                            OPCITEMRESULT result = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
                            ItemServerHandle[0] = result.hServer;
                        }
                        if (errors[1] == 0)
                        {
                            pos = new IntPtr(pos.ToInt32() + Marshal.SizeOf(typeof(OPCITEMRESULT)));
                            OPCITEMRESULT result = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
                            ItemServerHandle[1] = result.hServer;
                        }
                    }
                    catch(System.Exception error)
                    {
                        MessageBox.Show(error.Message, "Result-Adding Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        //free the memory
                        if (pResults != IntPtr.Zero)
                        {
                            Marshal.FreeCoTaskMem(pResults);
                            pResults = IntPtr.Zero;
                        }
                        if (pErrors != IntPtr.Zero)
                        {
                            Marshal.FreeCoTaskMem(pErrors);
                            pErrors = IntPtr.Zero;
                        }
                    }
                }
                catch(System.Exception error)
                {
                    MessageBox.Show(String.Format("Error while creating group object:-{0}", error.Message), "Result-Add group", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                }
                finally
                {
                    if (hDeadband.IsAllocated) hDeadband.Free();
                    if (hTimeBias.IsAllocated) hTimeBias.Free();
                }
            }
            catch(System.Exception error)
            {
                MessageBox.Show(String.Format("Error while creating server object:-{0}", error.Message), "Result-Create Server", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }

        //同步读数据
        private void Btn_Read_Click(object sender, EventArgs e)
        {
            IntPtr pItemValues = IntPtr.Zero;
            IntPtr pErrors = IntPtr.Zero;
            try
            {
                IOPCSyncIO20Obj.Read(OPCDATASOURCE.OPC_DS_DEVICE, 2, ItemServerHandle, out pItemValues, out pErrors);
                int[] errors = new int[2];
                Marshal.Copy(pErrors, errors, 0, 2);
                OPCITEMSTATE[] pItemState = new OPCITEMSTATE[2];
                if (errors[0] == 0)
                {
                    pItemState[0] = (OPCITEMSTATE)Marshal.PtrToStructure(pItemValues, typeof(OPCITEMSTATE));
                    pItemValues = new IntPtr(pItemValues.ToInt32() + Marshal.SizeOf(typeof(OPCITEMSTATE)));
                    Txt_R1Value.Text = String.Format("{0}", pItemState[0].vDataValue);
                    Txt_R1Quality.Text = GetQuality(pItemState[0].wQuality);
                    DateTime dt = ToDateTime(pItemState[0].ftTimeStamp);
                    Txt_R1TimeStamp.Text = dt.ToString();
                }
                if (errors[1] == 0)
                {
                    pItemState[1] = (OPCITEMSTATE)Marshal.PtrToStructure(pItemValues, typeof(OPCITEMSTATE));
                    pItemValues = new IntPtr(pItemValues.ToInt32() + Marshal.SizeOf(typeof(OPCITEMSTATE)));
                    Txt_R2Value.Text = String.Format("{0}", pItemState[1].vDataValue);
                    Txt_R2Quality.Text = GetQuality(pItemState[1].wQuality);
                    DateTime dt = ToDateTime(pItemState[1].ftTimeStamp);
                    Txt_R2TimeStamp.Text = dt.ToString();
                }
            }
            catch(System.Exception error)
            {
                MessageBox.Show(error.Message, "Result-Read Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //free the memory
                if (pItemValues != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pItemValues);
                    pItemValues = IntPtr.Zero;
                }
                if (pErrors != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pErrors);
                    pErrors = IntPtr.Zero;
                }
            }
        }

        //同步写数据
        private void Btn_Write_Click(object sender, EventArgs e)
        {
            IntPtr pErrors = IntPtr.Zero;
            object[] values = new object[2];
            values[0] = Txt_W1.Text;
            values[1] = Txt_W2.Text;
            try
            {
                IOPCSyncIO20Obj.Write(2, ItemServerHandle, values, out pErrors);
                int[] errors = new int[2];
                Marshal.Copy(pErrors, errors, 0, 2);
                String pstrError;
                String pstrError1;
                ServerObj.GetErrorString(errors[0], LOCALE_ID, out pstrError);
                ServerObj.GetErrorString(errors[1], LOCALE_ID, out pstrError1);
            }
            catch(System.Exception error)
            {
                MessageBox.Show(error.Message, "Result-WriteItem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (pErrors != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(pErrors);
                    pErrors = IntPtr.Zero; ;
                }
            }
        }
        private String GetQuality(long wQuality)    //质量码
        {
            String strQuality = "";
            switch (wQuality)
            {
                case Qualities.OPC_QUALITY_GOOD:
                    strQuality = "Good";
                    break;
                case Qualities.OPC_QUALITY_BAD:
                    strQuality = "Bad";
                    break;
                case Qualities.OPC_QUALITY_CONFIG_ERROR:
                    strQuality = "BadConfigurationError";
                    break;
                case Qualities.OPC_QUALITY_NOT_CONNECTED:
                    strQuality = "BadNotConnected";
                    break;
                case Qualities.OPC_QUALITY_DEVICE_FAILURE:
                    strQuality = "BadDeviceFailure";
                    break;
                case Qualities.OPC_QUALITY_SENSOR_FAILURE:
                    strQuality = "BadSensorFailure";
                    break;
                case Qualities.OPC_QUALITY_COMM_FAILURE:
                    strQuality = "BadCommFailure";
                    break;
                case Qualities.OPC_QUALITY_OUT_OF_SERVICE:
                    strQuality = "BadOutOfService";
                    break;
                case Qualities.OPC_QUALITY_WAITING_FOR_INITIAL_DATA:
                    strQuality = "BadWaitingForInitialData";
                    break;
                case Qualities.OPC_QUALITY_EGU_EXCEEDED:
                    strQuality = "UncertainEGUExceeded";
                    break;
                case Qualities.OPC_QUALITY_SUB_NORMAL:
                    strQuality = "UncertainSubNormal";
                    break;
                default:
                    strQuality = "Not handled";
                    break;
            }
            return strQuality;
        }
        private DateTime ToDateTime(OpcRcw.Da.FILETIME ft)
        {
            long highbuf = (long)ft.dwHighDateTime;
            long buffer = (highbuf << 32) + ft.dwLowDateTime;
            return DateTime.FromFileTimeUtc(buffer);
        }

        private void Btn_DisConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IOPCSyncIO20Obj != null)
                {
                    Marshal.ReleaseComObject(IOPCSyncIO20Obj);
                    IOPCSyncIO20Obj = null;
                }
                ServerObj.RemoveGroup(pSvrGroupHandle, 0);
                if (IOPCGroupStateMgtObj != null)
                {
                    Marshal.ReleaseComObject(IOPCGroupStateMgtObj);
                    IOPCGroupStateMgtObj = null;
                }
                if (MyobjGroup1 != null)
                {
                    Marshal.ReleaseComObject(MyobjGroup1);
                    MyobjGroup1 = null;
                }
                if (ServerObj != null)
                {
                    Marshal.ReleaseComObject(ServerObj);
                    ServerObj = null;
                }
            }
            catch(System.Exception error)
            {
                MessageBox.Show(error.Message, "Result-Stop Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
