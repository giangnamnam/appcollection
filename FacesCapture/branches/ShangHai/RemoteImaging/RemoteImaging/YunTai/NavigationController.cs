using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavigationControl;
using Damany.Util.Extensions;
using System.Windows.Forms;
using System.IO;
using Damany.PC.Domain;
namespace RemoteImaging.YunTai
{
    public class NavigationController
    {
        private readonly INavigationScreen _screen;
        private readonly ConnectionManager _connectionManager
            = new ConnectionManager();
       
         
        public const string CmdNavLeftUp = "NavLeftUp";
        public const string CmdNavLeftDown = "NavLeftDown";
        public const string CmdNavRightUp = "NavRightUp";
        public const string CmdNavRightDown = "NavRightDown";
        public const string CmdDefultPos = "DefultPos";
        public const string CmdReturnDefultPos = "ReturnDefultPos";
        public const string CmdCameraFocusIn = "CameraFocusIn";
        public const string CmdCameraFocusOut = "CameraFocusOut";
        //雨伞
        public const string CmdCameraSwitchOn = "CameraSwitchOn";
        public const string CmdCameraSwitchOff = "CameraSwitchOff";
        //CameraIrisOpen
        public const string CmdCameraIrisOpen = "CameraIrisOpen";
        public const string CmdCameraIrisClose = "CameraIrisClose";
        //CameraZoomWide
        public const string CmdCameraZoomWide = "CameraZoomWide";
        public const string CmdCameraZoomTele = "CameraZoomTele";

        public const string CmdNavLeft = "NavLeft";
        public const string CmdNavRight = "NavRight";
        public const string CmdNavUp = "NavUp";
        public const string CmdNavDown = "NavDown";
        public const string CmdStop = "Stop";

       

        //CameraFocusSub
         //CameraFocusAdd

         private readonly Dictionary<string, IEnumerable<INavigationCommand>>
            _commands = new Dictionary<string, IEnumerable<INavigationCommand>>();


        public NavigationController(INavigationScreen screen)
        {
            if (screen == null) throw new ArgumentNullException("screen");
            _screen = screen;
            
            RegisterCommands();
        }

        public void Start()
        {
            _screen.AttachController(this);
        }

        public void NavLeft()
        {
            
                ExeCuteCommand(CmdNavLeft);
          
        }
        public void NavLeftUp()
        {

            ExeCuteCommand(CmdNavLeftUp);

        }
        public void NavLeftDown()
        {

            ExeCuteCommand(CmdNavLeftDown);

        }
        public void NavRightUp()
        {
            ExeCuteCommand(CmdNavRightUp);
        }
        public void NavRightDown()
        {
            ExeCuteCommand(CmdNavRightDown);
        }
        public void NavRight()
        {
            ExeCuteCommand(CmdNavRight);
        }

        public void NavUp()
        {
            ExeCuteCommand(CmdNavUp);
        }


        public void NavDown()
        {
            ExeCuteCommand(CmdNavDown);
        }
        public void NavDefultPos()
        {
            
            Damany.Cameras.SanyoNetCamera oSany = new Damany.Cameras.SanyoNetCamera();
            try
            {
                ExeCuteCommand(CmdDefultPos);
                
                oSany.Uri = CurrentCamera.Location;
                oSany.UserName = "admin";
                oSany.PassWord = "admin";
                oSany.Connect();
                oSany.SetDefaultPosition();
                oSany.Close();
               
               
            }
            catch (Exception ex)
            {
                oSany.writeLog(ex.Message+ " "+ex.StackTrace);
            }
        }
        public void NavReturnDefultPos()
        {
            
            Damany.Cameras.SanyoNetCamera oSany = new Damany.Cameras.SanyoNetCamera();
            try
            {
                ExeCuteCommand(CmdReturnDefultPos);
                oSany.Uri = CurrentCamera.Location;
                oSany.UserName = "admin";
                oSany.PassWord = "admin";
                oSany.Connect();
                oSany.ReturnDefaultPosition();
                oSany.Close();
                
               
            }
            catch (Exception ex)
            {
                oSany.writeLog(ex.Message+ " "+ex.StackTrace);
            }

        }
        #region 聚焦控制
        public void NavCameraFocusIn()
        {
            ExeCuteCommand(CmdCameraFocusIn);
            Damany.Cameras.SanyoNetCamera oSany = new Damany.Cameras.SanyoNetCamera();
            try
            {

                oSany.Uri = CurrentCamera.Location;
                oSany.UserName = "admin";
                oSany.PassWord = "admin";
                oSany.Connect();
                oSany.SetFocusIn();
                oSany.Close();
            }
            catch (Exception ex)
            {
                oSany.writeLog(ex.Message + " " + ex.StackTrace);
            }
        }
        public void NavCameraFocusOut()
        {
            ExeCuteCommand(CmdCameraFocusOut);
            Damany.Cameras.SanyoNetCamera oSany = new Damany.Cameras.SanyoNetCamera();
            try
            {

                oSany.Uri = CurrentCamera.Location;
                oSany.UserName = "admin";
                oSany.PassWord = "admin";
                oSany.Connect();
                oSany.SetFocusOut();
                oSany.Close();
            }
            catch (Exception ex)
            {
                oSany.writeLog(ex.Message + " " + ex.StackTrace);
            }
        }
        #endregion
        #region 雨刷控制
        public void NavCameraSwitchOn()
        {
            ExeCuteCommand(CmdCameraSwitchOn);
        }
        public void NavCameraSwitchOff()
        {
            ExeCuteCommand(CmdCameraSwitchOff);
        }
        #endregion
        #region 光圈控制
        public void NavCameraIrisOpen()
        {
            ExeCuteCommand(CmdCameraIrisOpen);
            Damany.Cameras.SanyoNetCamera oSany = new Damany.Cameras.SanyoNetCamera();
            try
            {

                oSany.Uri = CurrentCamera.Location;
                oSany.UserName = "admin";
                oSany.PassWord = "admin";
                oSany.Connect();
                oSany.SetIrisOpen();
                oSany.Close();
            }
            catch (Exception ex)
            {
                oSany.writeLog(ex.Message + " " + ex.StackTrace);
            }
        }
        public void NavCameraIrisClose()
        {
            ExeCuteCommand(CmdCameraIrisClose);
            Damany.Cameras.SanyoNetCamera oSany = new Damany.Cameras.SanyoNetCamera();
            try
            {

                oSany.Uri = CurrentCamera.Location;
                oSany.UserName = "admin";
                oSany.PassWord = "admin";
                oSany.Connect();
                oSany.SetIrisClose();
                oSany.Close();
            }
            catch (Exception ex)
            {
                oSany.writeLog(ex.Message + " " + ex.StackTrace);
            }
        }
        #endregion

        #region 对焦控制
        public void NavCameraZoomWide()
        {
            ExeCuteCommand(CmdCameraZoomWide);
        }
        public void NavCameraZoomTele()
        {
            ExeCuteCommand(CmdCameraZoomTele);
        }
        #endregion
        public void NavStop()
        {
            var cmd = _commands[CmdStop];
            SendCommand(cmd);
        }

        public void ExeCuteCommand(string commandName)
        {
            
               
            var cmd = _commands[commandName];
           
            SendCommand(cmd);
            
        }

        public void RegisterCommand(string commandName, IEnumerable<INavigationCommand> commands)
        {
            if (_commands.ContainsKey(commandName))
            {
                throw new ArgumentException("key already exist");
            }

            _commands.Add(commandName, commands);
        }

        public IEnumerable<INavigationCommand> this[string commandName]
        {
           get
           {
               return _commands[commandName];
           }
        }


        private void RegisterCommands()
        {
            _commands.Add(CmdNavLeft, NavigationCommand.PanLeft.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavRight, NavigationCommand.PanRight.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavUp, NavigationCommand.PanUp.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavDown, NavigationCommand.PanDown.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdStop, NavigationCommand.Stop.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdDefultPos, NavigationCommand.SetPanDefultPos.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdReturnDefultPos, NavigationCommand.SetPanReturnDefultPos.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraFocusIn, NavigationCommand.CameraFocusIn.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraFocusOut, NavigationCommand.CameraFocusOut.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavLeftUp, NavigationCommand.PanLeftUp.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavLeftDown, NavigationCommand.PanLeftDown.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavRightUp, NavigationCommand.PanRightUp.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdNavRightDown, NavigationCommand.PanRightDown.AsEnumerable<INavigationCommand>());
  
            _commands.Add(CmdCameraSwitchOn, NavigationCommand.CameraSwitchOn.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraSwitchOff, NavigationCommand.CameraSwitchOff.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraIrisOpen, NavigationCommand.CameraIrisOpen.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraIrisClose, NavigationCommand.CameraIrisClose.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraZoomWide, NavigationCommand.CameraZoomWide.AsEnumerable<INavigationCommand>());
            _commands.Add(CmdCameraZoomTele, NavigationCommand.CameraZoomTele.AsEnumerable<INavigationCommand>());



        }

        public CameraInfo CurrentCamera { get; set; }
        private void SendCommand(IEnumerable<INavigationCommand> commands)
        {

            var cam = _screen.SelectedCamera();
            CurrentCamera = cam;
            commands.AddressTo((byte)cam.YunTaiId);
            var buffer = commands.Build();
           
            var stream = _connectionManager.GetConnection(cam.YunTaiUri);
            stream.Write(buffer, 0, buffer.Length);
           
        }
    }
}
