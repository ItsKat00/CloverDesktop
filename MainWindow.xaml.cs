using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Drawing;
using Microsoft.Win32;
using NotifyIcon = System.Windows.Forms.NotifyIcon;
using Form = System.Windows.Forms.Form;
using Screen = System.Windows.Forms.Screen;
using TrackBar = System.Windows.Forms.TrackBar;
using FormBorderStyle = System.Windows.Forms.FormBorderStyle;
using FormStartPosition = System.Windows.Forms.FormStartPosition;
using ContextMenu = System.Windows.Forms.ContextMenu;
using DispatcherTimer = System.Windows.Threading.DispatcherTimer;
using Control = System.Windows.Forms.Control;
using RichTextBox = System.Windows.Forms.RichTextBox;
using Button = System.Windows.Forms.Button;
using Label = System.Windows.Forms.Label;
using CheckBox = System.Windows.Forms.CheckBox;
using Size = System.Drawing.Size;
using MenuItem = System.Windows.Forms.MenuItem;

namespace WPF_CloverApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] logArr = new string[1000]; // 1000 events until they're just overwritten.
        private int currLog = 0;
        private bool isFoxMoving = false;
        private bool pauseMovement = true;
        private int target = 0;
        private int xMinPos, xMaxPos;
        private int mvFrequency = 3;
        private bool enableDebug = false;

        private readonly DispatcherTimer timerMoveClover = new DispatcherTimer();
        private readonly DispatcherTimer timerAnimate = new DispatcherTimer();
        private readonly NotifyIcon notifyIcon = new NotifyIcon();
        private readonly MenuItem miDebug = new MenuItem();

        public MainWindow()
        {
            InitializeComponent();
            log("init", "info", "app started.");
            SystemEvents.DisplaySettingsChanged += delegate { placeImg(); }; // testing shows that it moves on its own, but this is here just to force to anyway.
            System.Windows.Forms.Application.EnableVisualStyles(); // here for the info box. the rest of the custom forms have them in their functions.

            timerMoveClover.Tick += delegate { chooseWhereMove(); }; // timer to trigger potentially moving clover
            timerMoveClover.Interval = new TimeSpan(0, 0, 0, 0, 5000);
            timerMoveClover.Start();

            timerAnimate.Tick += delegate { moveTheFox(); }; // timer for how fast clover moves
            timerAnimate.Interval = new TimeSpan(0, 0, 0, 0, 50);
            anim.ImageFailed += delegate { log("anim", "CRIT", "ImageFailed called."); createLogForm(); };

            Window.Topmost = Properties.Settings.Default.Topmost;
            Window.ShowInTaskbar = Properties.Settings.Default.ShowInTaskbar;
            enableDebug = Properties.Settings.Default.EnableDebug;
            mvFrequency = Properties.Settings.Default.mvFrequency;

            initNotifyIcon();
            placeImg();
        }

        private void anim_Loaded(object sender, RoutedEventArgs e)
        {
            pauseMovement = false;
            loadingText.Visibility = Visibility.Hidden;
            log("animLoaded", "info", "finished loading img");
        }

        private void placeImg()
        {
            log("placeImg", "info", "reset form location to application default.");
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea; // primary screen since that's the default screen to place on
            Screen[] screens = Screen.AllScreens;

            log("placeImg", "info", "screens detected: " + screens.Length);
            xMaxPos = 0;
            xMinPos = 0;
            for (int i = 0; i < screens.Length; i++)
            {
                Rectangle scr = screens[i].WorkingArea;
                log("placeImg", "info", "screen " + i + " parameters: " + scr.Width + "x" + scr.Height);

                if (scr.X < xMinPos)
                    xMinPos = scr.X;
                if (scr.X > xMaxPos)
                    xMaxPos = scr.X;
                xMaxPos += workingArea.Width;
            }
            log("placeImg", "info", "screen coordinates: xMinPos of " + xMinPos + " and xMaxPos of " + xMaxPos);

            Window.Left = workingArea.Left + workingArea.Width - 120;
            Window.Top = workingArea.Top + workingArea.Height - 133;
        }

        private void imgMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                createInfoForm();
            timerAnimate.Stop(); // it's hard to double click when moving, so stop moving on the first click.
            isFoxMoving = false;
            log("mdEvent", "info", "stopped any movement.");
        }

        private void moveTheFox()
        {
            isFoxMoving = true; // lock
            timerAnimate.Start();
            if (Window.Left != target) // needs some fine-tuning to smooth out the scoot. also, fix issue where clover goes off screen.
            {
                if (target > Window.Left)
                    Window.Left += 2;
                else
                    Window.Left -= 2;
            }
            else
            {
                isFoxMoving = false;
                timerAnimate.Stop();
                timerMoveClover.Start();
                log("animTimer", "info", "finished moving.");
            }
        }

        private void chooseWhereMove()
        {
            if (isFoxMoving || pauseMovement || mvFrequency == 0)
                return;
            Random rnd = new Random();
            // look away, im commiting sin
            int[] invertedNumbers = { 0, 9, 7, 5, 3, 1 };
            if (rnd.Next(1, invertedNumbers[mvFrequency]) == 1)
            {
                log("startAnim", "info", "starting move with mvF of " + mvFrequency + " and IN of " + invertedNumbers[mvFrequency].ToString());
                target = (int)Math.Ceiling((double)rnd.Next((int)(Window.Left - 500), (int)(Window.Left + 500)) / 2) * 2; // don't let clover see this math
                while (target == 0)
                {
                    if (target > (xMaxPos - 200))
                        target = target - (target - xMaxPos) - (int)Window.Width; // scoot clover back into a visible region
                    if (target < xMinPos && xMinPos != 0)
                        target = (target - (target - xMinPos)); // scoot clover back into a visible region. don't need to account for window width here.
                }

                log("startAnim", "info", "starting to move clover with target of " + target + " with current location of " + Window.Left + ", difference of " + (target - Window.Left));
                timerAnimate.Start();
                timerMoveClover.Stop();
            }
        }

        #region CustomForms

        Form ab = new FormInfo();
        private void createInfoForm()
        {
            if (ab.IsDisposed) // it's been opened and closed previously. make new form and present.
                ab = new FormInfo();
            ab.Show();

            ab.Icon = new Icon("cloverchungus.ico");

            Control[] abBtnClose = ab.Controls.Find("btnClose", true);
            Control[] abBtnDebug = ab.Controls.Find("btnDebug", true);
            Control[] abBtnSettings = ab.Controls.Find("btnSettings", true);

            if (abBtnClose.Length == 0 || abBtnDebug.Length == 0 || abBtnSettings.Length == 0)
            {
                log("mdEvent", "WARN", "!!!"); // useless
            }
            try
            {
                abBtnClose[0].Click += delegate { closeApp(); };
                abBtnDebug[0].Click += delegate { createLogForm(); };
                abBtnSettings[0].Click += delegate { createSettingsForm(); };
            }
            catch (Exception ex)
            {
                log("mdEvent", "CRIT", "error while adding event handlers to info box. one or more buttons may be inoperable.\n" + ex.Message + "\n" + ex.StackTrace);
                createLogForm();
            }
        }

        Form dbgForm = new Form();
        private void createLogForm() // home made, from scratch debug window using fresh ingredients.
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            if (dbgForm.IsDisposed)
                dbgForm = new Form();

            log("createLW", "info", "creating log window.");
            dbgForm.Size = new Size(800, 400);
            dbgForm.Icon = new Icon("cloverchungus.ico");

            RichTextBox tb = new RichTextBox();
            Button btnForcePlacement = new Button();
            Button btnUpdateLog = new Button();

            tb.Size = new Size(700, 400);
            tb.Text = string.Join("\n", logArr); // add support for automatically refreshing text box someday.

            btnForcePlacement.Text = "reset location";
            btnForcePlacement.Width = 80;
            btnForcePlacement.Location = new System.Drawing.Point(702, 0);
            btnForcePlacement.Click += delegate { placeImg(); };

            btnUpdateLog.Text = "update log";
            btnUpdateLog.Width = 80;
            btnUpdateLog.Location = new System.Drawing.Point(702, 25);
            btnUpdateLog.Click += delegate { tb.Text = string.Join("\n", logArr); };

            dbgForm.Controls.Add(tb);
            dbgForm.Controls.Add(btnForcePlacement);
            dbgForm.Controls.Add(btnUpdateLog);

            dbgForm.StartPosition = FormStartPosition.CenterScreen;
            dbgForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            dbgForm.Show();
        }

        Form settingsForm = new Form();
        private void createSettingsForm()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            if (settingsForm.IsDisposed)
                settingsForm = new Form();
            settingsForm.Size = new Size(164, 188); // small? yes. weird? yes. temporary? hopefully not. "a temporary solution is the most permanant" - everyone
            settingsForm.StartPosition = FormStartPosition.CenterScreen;
            settingsForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            settingsForm.MinimizeBox = false;
            settingsForm.MaximizeBox = false;
            settingsForm.Text = "Settings";
            settingsForm.Icon = new Icon("cloverchungus.ico");

            TrackBar barFrequency = new TrackBar();
            Label labelTbTitle = new Label();
            Label labelTbGuideOff = new Label();
            Label labelTbGuideMost = new Label();
            CheckBox cbEnableDebug = new CheckBox();
            CheckBox cbTopmost = new CheckBox();
            CheckBox cbShowInTaskbar = new CheckBox();

            barFrequency.Maximum = 5;
            barFrequency.Size = new Size(120, 45);
            barFrequency.Location = new System.Drawing.Point(12, 25);
            barFrequency.Value = mvFrequency;
            barFrequency.ValueChanged += delegate { 
                mvFrequency = barFrequency.Value; 
                Properties.Settings.Default.mvFrequency = mvFrequency; 
                Properties.Settings.Default.Save(); 
            };

            labelTbTitle.Location = new System.Drawing.Point(12, 9);
            labelTbTitle.Text = "Movement Frequency";
            labelTbTitle.Width = 150;

            labelTbGuideOff.Location = new System.Drawing.Point(12, 57);
            labelTbGuideOff.Text = "Off";

            labelTbGuideMost.Location = new System.Drawing.Point(101, 57);
            labelTbGuideMost.Text = "Most";

            cbEnableDebug.Location = new System.Drawing.Point(12, 76);
            cbEnableDebug.Text = "Enable debug";
            cbEnableDebug.Checked = enableDebug;
            cbEnableDebug.CheckedChanged += delegate { 
                enableDebug = cbEnableDebug.Checked; 
                Properties.Settings.Default.EnableDebug = enableDebug; 
                Properties.Settings.Default.Save();
                miDebug.Visible = enableDebug;
                ab.Dispose();
                createInfoForm();
                settingsForm.BringToFront();
            };

            cbTopmost.Location = new System.Drawing.Point(12, 100);
            cbTopmost.Text = "Keep topmost";
            cbTopmost.Checked = Window.Topmost;
            cbTopmost.CheckedChanged += delegate { 
                Window.Topmost = cbTopmost.Checked; 
                Properties.Settings.Default.Topmost = Window.Topmost; 
                Properties.Settings.Default.Save();
            };

            cbShowInTaskbar.Location = new System.Drawing.Point(12, 123);
            cbShowInTaskbar.Text = "Show in taskbar";
            cbShowInTaskbar.Checked = Window.ShowInTaskbar;
            cbShowInTaskbar.CheckedChanged += delegate { 
                Window.ShowInTaskbar = cbShowInTaskbar.Checked; 
                Properties.Settings.Default.ShowInTaskbar = Window.ShowInTaskbar; 
                Properties.Settings.Default.Save(); 
            };

            settingsForm.Controls.Add(barFrequency);
            settingsForm.Controls.Add(labelTbTitle);
            settingsForm.Controls.Add(labelTbGuideOff);
            settingsForm.Controls.Add(labelTbGuideMost);
            settingsForm.Controls.Add(cbEnableDebug);
            settingsForm.Controls.Add(cbTopmost);
            settingsForm.Controls.Add(cbShowInTaskbar);
            labelTbGuideOff.BringToFront();
            labelTbGuideMost.BringToFront();
            settingsForm.Show();
        }

        private void initNotifyIcon()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem miAbout = new MenuItem();
            MenuItem miSettings = new MenuItem();
            MenuItem miResetLocation = new MenuItem();
            MenuItem miClose = new MenuItem();

            miAbout.Text = "About";
            miAbout.Click += delegate { createInfoForm(); };

            miSettings.Text = "Settings";
            miSettings.Click += delegate { createSettingsForm(); };

            miResetLocation.Text = "Reset location";
            miResetLocation.Click += delegate { placeImg(); };

            miDebug.Text = "Debug log";
            miDebug.Click += delegate { createLogForm(); };
            miDebug.Visible = enableDebug;

            miClose.Text = "Exit";
            miClose.Click += delegate { closeApp(); };

            contextMenu.MenuItems.Add(miAbout);
            contextMenu.MenuItems.Add(miSettings);
            contextMenu.MenuItems.Add(miResetLocation);
            contextMenu.MenuItems.Add(miDebug);
            contextMenu.MenuItems.Add(miClose);

            notifyIcon.Icon = new Icon("cloverchungus.ico");
            notifyIcon.Text = "Clover Desktop Companion";
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Visible = true;
        }
        #endregion

        private void log(string title, string severety, string message) // eventually log to a file in case of a crash.
        {
            StringBuilder sb = new StringBuilder();
            DateTime dateTime = DateTime.Now;
            sb.Append("[" + dateTime + "] ");
            sb.Append("[" + title + "] ");
            sb.Append("[" + severety + "]: ");
            sb.Append(message);
            logArr[currLog] = sb.ToString();
            currLog++;
            if (currLog > 999)
                currLog = 0;
        }
        private void closeApp()
        {
            notifyIcon.Dispose();
            Window.Close();
        }
    }
}