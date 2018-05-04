using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AzureFileUploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Firstly goto App.config and change the StorageConnectionString.

            //Mine looks like:   
            //<add key="StorageConnectionString" value="DefaultEndpointsProtocol=https;AccountName=usman;AccountKey=59jU00i4gOJs2mAluo2ze6A1JDzPQ/d1Gq0GJSTCppwlS4NaswwN4NpJ9MVPiZPcrXLi0/KoRiZpsr1fZFR4mQ==" />
            
            //Replace 'usman' with your Storage Name from Windows Azure.
            //Also replace Account Key with the Storage primary key.


            var file = new Microsoft.Win32.OpenFileDialog();

            var result = file.ShowDialog();
            if (result == false)
                return;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
      
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer"); //Replace "mycontainer" with the name of your container.

            CloudBlockBlob blob = container.GetBlockBlobReference(file.SafeFileName); //This is where you set the filename. Currently, it will the the uploaded file's name.

            using (var fileStream = System.IO.File.OpenRead(file.FileName)) //File read and stream to Azure.
            {
                blob.UploadFromStream(fileStream);
            }
            MessageBox.Show("File Uploaded Succesfully!");
        }
    }
}
