using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Libreria que nos permite manejar los archivos y directorios
using System.IO;
//obtener mac
using System.Net.NetworkInformation;

namespace Manage_Files
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //C:\\Users\\adminfoco\\Desktop\\Manage_files\\Manage_Files\\Manage_Files\\bin\\Debug

            //Obtenego path de la carpeta donde me encuentro ubicado al momento
            String path = Directory.GetCurrentDirectory();
            //corto la cadena de caracteres en base el caracter (\)
            var aux = path.Split('\\');
            String pth = aux[0];

            for (int i = 1; i < aux.Length-2; i++)
            {
                pth = pth + "\\" + aux[i];
            }

            pth = pth + "\\files";

            //verifico si la carpeta existe
            if (Directory.Exists(pth))
            {
                MessageBox.Show("Existe la carpeta files");
                //leer archivo
                String message = File.ReadAllText(pth + "\\bienvenida.txt");
                MessageBox.Show(message);
            }
            else
            {
                //si no existe la carpeta se crea
               var dirctory = Directory.CreateDirectory(pth);
               //MessageBox.Show(dirctory.FullName);
               //MessageBox.Show("Se creo la carpeta files");
                //Creo archivo
                TextWriter txt = new StreamWriter(pth+"\\bienvenida.txt");
                txt.Write("Hola usuario, ingresa un mensaje.");
                txt.Close();

                //leer archivo
                String message = File.ReadAllText(pth + "\\bienvenida.txt");
                MessageBox.Show(message);


            }

        }

        private void btn_save_file_Click(object sender, EventArgs e)
        {
            String path = Directory.GetCurrentDirectory();
            //corto la cadena de caracteres en base el caracter (\)
            var aux = path.Split('\\');
            String pth = aux[0];

            for (int i = 1; i < aux.Length - 2; i++)
            {
                pth = pth + "\\" + aux[i];
            }

            pth = pth + "\\files";

            if (File.Exists(pth + "\\message.txt"))
            {
                var ux = new Guid();

                TextWriter txt = new StreamWriter(pth + "\\message"+ ux + ".txt");
                txt.Write(rtxtMessage.Text);
                txt.Close();
            }
            else
            {
                TextWriter txt = new StreamWriter(pth + "\\message.txt");
                txt.Write(rtxtMessage.Text);
                txt.Close();
            }

            

            String firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();

            TextWriter txt2 = new StreamWriter(pth + "\\mac.xyz");
            txt2.Write(firstMacAddress);
            txt2.Close();

        }
    }
}
