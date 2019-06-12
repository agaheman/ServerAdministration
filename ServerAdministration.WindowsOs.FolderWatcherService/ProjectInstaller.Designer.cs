namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.folderWathcerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.folderWathcerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // folderWathcerServiceProcessInstaller
            // 
            this.folderWathcerServiceProcessInstaller.Password = null;
            this.folderWathcerServiceProcessInstaller.Username = null;
            this.folderWathcerServiceInstaller.Description = "سرویس رصد حجم درایو";

            // 
            // folderWathcerServiceInstaller
            // 
            this.folderWathcerServiceInstaller.ServiceName = "EppadFolderWatcherService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.folderWathcerServiceProcessInstaller,
            this.folderWathcerServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller folderWathcerServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller folderWathcerServiceInstaller;
    }
}