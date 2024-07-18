using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DirectShowLib;

namespace UvcCameraApp
{
    public partial class MainForm : Form
    {
        private IGraphBuilder graphBuilder;
        private ICaptureGraphBuilder2 captureGraphBuilder;
        private IMediaControl mediaControl;
        private IVideoWindow videoWindow;
        private IBaseFilter videoRenderer;
        private DsDevice[] systemCameras;
        private IBaseFilter sourceFilter; // フィールドとして宣言

        public MainForm()
        {
            InitializeComponent();
            LoadCameras();
        }

        private void LoadCameras()
        {
            // Get the list of available video input devices (cameras)
            systemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            foreach (var camera in systemCameras)
            {
                comboBoxCameras.Items.Add(camera.Name);
            }
            if (comboBoxCameras.Items.Count > 0)
            {
                comboBoxCameras.SelectedIndex = 0; // Select the first camera by default
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            InitializeDirectShow(comboBoxCameras.SelectedIndex);
        }

        private void InitializeDirectShow(int cameraIndex)
        {
            try
            {
                int hr = 0;
                // Create the Filter Graph Manager
                graphBuilder = (IGraphBuilder)new FilterGraph();

                // Create the Capture Graph Builder
                captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
                captureGraphBuilder.SetFiltergraph(graphBuilder);

                // Check if there are any cameras
                if (systemCameras == null || systemCameras.Length == 0)
                {
                    MessageBox.Show("No UVC cameras found.");
                    return;
                }

                // Bind Moniker to a filter object
                object source;
                Guid baseFilterGuid = typeof(IBaseFilter).GUID;
                systemCameras[cameraIndex].Mon.BindToObject(null, null, ref baseFilterGuid, out source);
                sourceFilter = (IBaseFilter)source;

                // Add the video capture filter to the graph
                hr = graphBuilder.AddFilter(sourceFilter, "Video Capture");
                DsError.ThrowExceptionForHR(hr);

                // Automatically select and add the video renderer
                hr = captureGraphBuilder.RenderStream(PinCategory.Capture, MediaType.Video, sourceFilter, null, null);
                DsError.ThrowExceptionForHR(hr);

                // Get the Media Control interface
                mediaControl = (IMediaControl)graphBuilder;

                // Get the Video Window interface
                videoWindow = (IVideoWindow)graphBuilder;

                ConfigureVideoWindow();

                // Run the graph
                hr = mediaControl.Run();
                DsError.ThrowExceptionForHR(hr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing DirectShow: " + ex.Message);
            }
        }

        private void ConfigureVideoWindow()
        {
            try
            {
                // Set the video window to the panelVideo control
                int hr = videoWindow.put_Owner(panelVideo.Handle);
                DsError.ThrowExceptionForHR(hr);

                hr = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipSiblings);
                DsError.ThrowExceptionForHR(hr);

                hr = videoWindow.SetWindowPosition(0, 0, panelVideo.Width, panelVideo.Height);
                DsError.ThrowExceptionForHR(hr);

                hr = videoWindow.put_Visible(OABool.True);
                DsError.ThrowExceptionForHR(hr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error configuring video window: " + ex.Message);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // Release DirectShow resources
            if (mediaControl != null)
            {
                mediaControl.Stop();
            }
            if (videoRenderer != null)
            {
                Marshal.ReleaseComObject(videoRenderer);
            }
            if (videoWindow != null)
            {
                videoWindow.put_Visible(OABool.False);
                videoWindow.put_Owner(IntPtr.Zero);
            }
            if (graphBuilder != null)
            {
                Marshal.ReleaseComObject(graphBuilder);
            }
            if (captureGraphBuilder != null)
            {
                Marshal.ReleaseComObject(captureGraphBuilder);
            }
        }
    }
}
