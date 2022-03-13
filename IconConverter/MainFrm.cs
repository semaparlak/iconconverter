namespace IconConverter
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            comboBoxPreviewMode.SelectedIndex = 0;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Supported Image Files|*.jpg;*.png;*.bmp;*.ico";
            openFile.Title = "Select Images";
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                foreach (string FileName in openFile.FileNames)
                {
                    listBoxFiles.Items.Add(FileName);
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                folderBrowser.Description = "Please select the save folder.";
                folderBrowser.ShowNewFolderButton = true;
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    foreach (string FileLoc in listBoxFiles.Items)
                    {
                        string FileName = Path.GetFileNameWithoutExtension(FileLoc) + ".ico";
                        Image img = Image.FromFile(FileLoc);
                        img.Save(folderBrowser.SelectedPath + "\\" + FileName, System.Drawing.Imaging.ImageFormat.Icon);
                        i++;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                MessageBox.Show("Completed tasks:" + i.ToString() + "/" + listBoxFiles.Items.Count.ToString());
            }
        }

        private void comboBoxPreviewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mode = comboBoxPreviewMode.SelectedIndex;
            if (mode == 0)
            {
                pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (mode == 1)
            {
                pictureBoxPreview.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            else
            {
                pictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? FileName = listBoxFiles.Items[listBoxFiles.SelectedIndex].ToString();
            if (File.Exists(FileName))
            {
                pictureBoxPreview.Image = Image.FromFile(FileName);
            }
        }
    }
}