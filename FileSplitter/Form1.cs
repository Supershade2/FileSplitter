using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
/*!
 *  \file Form1.cs
 *  File that contains the code for the main form
 *  This file contains the source code for the main form the user will interact with
 */
namespace FileSplitter
{
    //! Primary form class
    /*! 
    The class is the main form the user will be interacting with.
    \n The types of global variables are string, assembly, IEnumerable, boolean, Integer32
    \n Global Variables in order of type:
    \n -cdir: variable containing the current directory the program is running from
    \n -assembly: variable that contains information about the current executing assembly
    \n -totalfiles: Integer that will store the total number of files to create
    */
    public partial class Form1 : Form
    {
        //string FileName;
        //string user_selected_file;
        string cdir = Directory.GetCurrentDirectory();
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        /*!*/
        IEnumerable<string> path;
        //! boolean related to the tabpage population method
        bool tabhidden = false;
        //! boolean that is used to check whether or not to create the autojoiner executable
        bool buildAutoJoiner = false;
        //! Integer that stores the index of the selected filemap
        int currentFileSelected;
        //! stores the code from the resource Template.cs
        string Source;
        int split = 0;
        /*! IEnumerable that is used to store the paths of the filemaps */
        public IEnumerable<string> filepaths;
        //! Form1 method
        /*! Entry point for the Form1 class that calls the method in the Form1 Designer class to generate the form*/
        public Form1()
        {
            Stream code;
            //@{
            /*! this expression if it evaulates to true will create the subdirectory to store the filemaps*/
            if (!Directory.Exists(cdir + @"\\maps"))
            {
                Directory.CreateDirectory(cdir + @"\\maps");
            }
            //@}
            //@{
            /*! Linq Query */
            path = (from item in assembly.GetManifestResourceNames() where (item.Contains(".cs")) select item);
			//@}
            code = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path.FirstOrDefault());
            StreamReader reader = new StreamReader(code,System.Text.Encoding.UTF8);
            Source = reader.ReadToEnd();
            path = null;
            code.Dispose();
            GC.Collect(GC.GetGeneration(code),GCCollectionMode.Forced);
            InitializeComponent();
        }
        int totalfiles;
        int currentphase = 0;
        string nextIterationStart = "";
        ProgressUpdate pform = new ProgressUpdate();
        System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();
        //button click event @{
        /*! opens the file explorer to pick file(s) to split up into multiple files*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //FileName = openFileDialog1.FileName;
                //string DirName = System.IO.Path.GetDirectoryName(FileName);
                //user_selected_file = FileName.Substring(DirName.Length + 1);
                /*foreach (string i in openFileDialog1.FileNames)
                {
                    byte[] filebytes;
                    filebytes = File.ReadAllBytes(Path.GetFullPath(openFileDialog1.FileName));
                    files.Add(filebytes);
                }*/
                label2.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
            }
        }
        //@} @{
        /*! The event that is executed when the split files button is clicked */
        private void button2_Click(object sender, EventArgs e)
        {
            byte[] filebytes = File.ReadAllBytes(openFileDialog1.FileNames[currentphase]);
            if (convertInput(textBox1.Text) != "")
            {
                split = Convert.ToInt32(convertInput(textBox1.Text)) != 0 ? Convert.ToInt32(convertInput(textBox1.Text)) : 1;
                totalfiles = radioButton1.Checked ? filebytes.Length / (filebytes.Length / split) : filebytes.Length/split;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    GC.Collect(GC.GetGeneration(pform), GCCollectionMode.Forced);
                    pform = new ProgressUpdate();
                    /*SHA256 hash = SHA256.Create();
                    for (int bi = 0; bi < files.Count; bi++)
                    {
                        byte[] h = hash.ComputeHash(files.ElementAt(bi), 0, files.ElementAt(bi).Length);
                        File.WriteAllBytes(Path.GetDirectoryName(saveFileDialog1.FileName) + @"\\Hash.sha256", h);
                    }*/
                    pform.maxvalue = totalfiles;
                    pform.Show();
                    buildAutoJoiner = checkBox1.Checked;
                    if (buildAutoJoiner)
                    {
                        if (!Directory.Exists(cdir + @"\\AutoJoiners\\"))
                        {
                            Directory.CreateDirectory(cdir + @"\\AutoJoiners\\");
                        }
                    }
                }
                backgroundWorker1.RunWorkerAsync();
                w.Start();
                Hide();
            }
            else 
            {
                //textBox1.BackColor = System.Drawing.Color.Red;
            }
        }
        //@}
        public string convertInput(string number)
        {
            char[] parse = number.ToCharArray();
            number = "";
            foreach (char e in parse)
            {
                if ((int)e > 47 && (int)e < 58)
                {
                    number += e;
                }
            }
            return number;
        }
        private string BuildFilePath(string dir, string[] fileparts)
        {
            dir += fileparts[0];
            for (int i = 1; i < fileparts.Length; i++)
            {
                dir = dir+'.'+fileparts[i];
            }
            return dir;
        }
        private string BuildFilePath(string dir, string[] fileparts, int number)
        {
            dir += fileparts[0]+number;
            for (int i = 1; i < fileparts.Length; i++)
            {
                dir = dir + '.' + fileparts[i];
            }
            return dir;
        }
        private string BuildFilePath(string dir, string iteration, string[] fileparts) 
        {
            dir += fileparts[0]+iteration;
            for (int i = 1; i < fileparts.Length; i++)
            {
                dir = dir + '.' + fileparts[i];
            }
            return dir;
        }
        private string BuildFilePath(string dir, string iteration, string[] fileparts,int number) 
        {
            dir += fileparts[0]+iteration+number;
            for (int i = 1; i < fileparts.Length; i++)
            {
                dir = dir + '.' + fileparts[i];
            }
            return dir;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //bool Ignore = false;
            System.Threading.Thread worker1Thread = new System.Threading.Thread(() =>
            {
            IEnumerable<string> maps = (from i in Directory.EnumerateFiles(cdir + @"\\maps\\") where (File.ReadAllLines(i).Count() > 0 && i.Contains(Path.GetFileName(openFileDialog1.FileNames[currentphase])) == false) select i);
            maps = (from i in maps where(File.ReadAllLines(i).First().Equals(Path.GetDirectoryName(saveFileDialog1.FileName+@"\")+Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()))select i);
            foreach (string item in maps) { File.Delete(item); }});
            worker1Thread.Start();
            var auto = ProjectRootElement.Create();
            var propertyGroup = auto.AddPropertyGroup();
            var slItemGroup = auto.CreateItemGroupElement();
            var sl1ItemGroup = auto.CreateItemGroupElement();
            var sourceitem = auto.CreateItemGroupElement();
            byte[] filebytes = File.ReadAllBytes(openFileDialog1.FileNames[currentphase]);
            int fileNumber = 2;
            //List<string> contents = new List<string>();
            //if (File.Exists(Path.GetDirectoryName(saveFileDialog1.FileName) + String.Format(@"\\" + "{0}FilePartsMap.txt", Path.GetFileName(openFileDialog1.FileName))) == false) 
            //{
            if(File.Exists(openFileDialog1.FileNames[currentphase])==true) 
            {
                using (FileStream cstream = File.Create(cdir + String.Format(@"\\maps\\" + "{0}.fmap", Path.GetFileName(openFileDialog1.FileNames[currentphase]))))
                {
                    cstream.Dispose();
                }
            }
                using (var file = File.Create(Path.GetDirectoryName(saveFileDialog1.FileName) + @"\\" + Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart+"." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()))
                {
                    file.Dispose();
                }
            if (buildAutoJoiner) 
            {
                propertyGroup.AddProperty("DefaultTargets", "Build");
                //auto.SetProperty("DefaultTargets","Build");
                auto.AddProperty("TargetFrameworkVersion", "v4.5");
                auto.AddProperty("RootNamespace", "AutoJoiner");
                auto.AddProperty("OutputType", "Exe");
                auto.InsertAfterChild(slItemGroup, auto.LastChild);
                slItemGroup.AddItem("Reference","System");
                slItemGroup.AddItem("Reference", "System.Core");
                slItemGroup.AddItem("Reference","System.Linq");
                slItemGroup.AddItem("Reference","System.Reflection");
                slItemGroup.AddItem("Reference","System.IO");
                slItemGroup.AddItem("Reference","System.Runtime.InteropServices");
                slItemGroup.AddItem("Reference","System.Collections.Generic");
                auto.InsertAfterChild(sl1ItemGroup,auto.LastChild);
            }
            //}
            /*using (var keyfile = File.Create(Path.GetDirectoryName(openFileDialog1.FileName) + @"\Hash.sha256"))
            {
                keyfile.Write(h, 0, h.Length);
                keyfile.Flush();
                keyfile.Dispose();
            }*/
            /*int hashindex = 0;
            for (int i = 0; i < filebytes.Length-1; i++)
            {
                filebytes[i] -= h[hashindex];
                hashindex = hashindex+1 < h.Length ? hashindex+1:0; 
            }*/
            //System.IO.File.CreateText("output").Write(ByteArrayToString(filebytes));
            var stream1 = File.Open(Path.GetDirectoryName(saveFileDialog1.FileName)+@"\"+Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart +"." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last(), FileMode.Open,FileAccess.Write,FileShare.Read);
            var writer = new BinaryWriter(stream1);
            //FileStream mapstream = File.OpenWrite(Path.GetDirectoryName(saveFileDialog1.FileName) + @"\\FilePartsMap.txt");
            //BinaryWriter mapwriter = new BinaryWriter(mapstream);
            //contents.AddRange(File.ReadAllLines(Path.GetDirectoryName(saveFileDialog1.FileName) + String.Format(@"\\"+"{0}FilePartsMap.txt",Path.GetFileName(openFileDialog1.FileName))));
            /*if (contents.Count > 1) 
            {
                string[] files = contents.ToArray();
                if (File.Exists(files[1]) == true && openFileDialog1.FileName == files[0]) 
                {
                    Ignore = true;
                }
            }*/
            //contents.Clear();
            //contents.Add(openFileDialog1.FileName);
            //pform.maxvalue = totalfiles;
            int index = 0;
                    /*while (true)
                    {*/
                        File.AppendAllText(cdir + String.Format(@"\\maps\\" + "{0}.fmap", Path.GetFileName(openFileDialog1.FileNames[currentphase])),Path.GetDirectoryName(saveFileDialog1.FileName) + @"\" + Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart +"." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()+Environment.NewLine);
            //contents.Add(Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last());
            /*List<System.Threading.Thread> filethreads = new List<System.Threading.Thread>();
            int threadnumber = 0;
            int incrementby = filebytes.Length / split;
            while (threadnumber < totalfiles) 
            {
                if (threadnumber == 0) 
                {
                    filethreads.Add(new System.Threading.Thread(() => {while(index<incrementby){ writer.BaseStream.WriteByte(filebytes[index]); }}));
                }
            }*/
            while (index < filebytes.Length)
                        {
                            if ((index + 1) % (filebytes.Length/split) == 0 && index != 0)
                            {
                                writer.BaseStream.Flush();
                                writer.BaseStream.Dispose();
                                /*if (File.Exists(buildFilePath(Path.GetDirectoryName(saveFileDialog1.FileName), (Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()).Split('.'), fileNumber)) == true)
                                {
                                    using (FileStream s = File.Create(buildFilePath(Path.GetDirectoryName(saveFileDialog1.FileName)+@"\", (Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()).Split('.'), fileNumber)))
                                    {
                                        s.Dispose();
                                    }
                                }*/
                                stream1 = File.OpenWrite(BuildFilePath(Path.GetDirectoryName(saveFileDialog1.FileName)+@"\", (Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()).Split('.'), fileNumber));
                                writer = new BinaryWriter(stream1);
                                //contents.Add(Ignore == false ? Path.GetFileName(buildFilePath(Path.GetDirectoryName(saveFileDialog1.FileName), saveFileDialog1.FileName.Substring(Path.GetDirectoryName(saveFileDialog1.FileName).Length).Split('.'), fileNumber)) + Environment.NewLine : "");
                                File.AppendAllText(cdir + String.Format(@"\\maps\\" + "{0}.fmap", Path.GetFileName(openFileDialog1.FileNames[currentphase])),BuildFilePath(Path.GetDirectoryName(saveFileDialog1.FileName)+@"\", (Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()).Split('.'), fileNumber)+Environment.NewLine);
                                writer.Write(filebytes[index]);
                                //index = index + 1 < filebytes.Length ? index + 1 : index;
                                fileNumber = fileNumber < totalfiles ? fileNumber + 1 : fileNumber;
                                index++;
                                backgroundWorker1.ReportProgress(fileNumber);
                            }
                                writer.BaseStream.WriteByte(filebytes[index]);
                                //index = index < filebytes.Length ? index + 1 : index;
                                //backgroundWorker1.ReportProgress(fileNumber/*((fileNumber < Int32.MaxValue ? fileNumber+1:fileNumber)/totalfiles)*/);
                                index++;
                            /*backgroundWorker1.ReportProgress(((fileNumber<Int32.MaxValue ? fileNumber + 1 : fileNumber) / totalfiles));
                            stream1 = File.OpenWrite(buildFilePath(Path.GetDirectoryName(saveFileDialog1.FileName), saveFileDialog1.FileName.Substring(Path.GetDirectoryName(saveFileDialog1.FileName).Length).Split('.'), fileNumber));
                            writer = new BinaryWriter(stream1);
                            writer.Write(filebytes[index]);
                            index = index + 1 < filebytes.Length ? index + 1 : index;
                            fileNumber = fileNumber < Int32.MaxValue ? fileNumber + 1 : fileNumber;*/
                        }
                        if (buildAutoJoiner) 
                        {
                            for (int i1 = 0; i1 < fileNumber; i1++)
                            {
                                if (i1 != 0)
                                {
                                    sl1ItemGroup.AddItem("EmbeddedResource", Path.GetDirectoryName(saveFileDialog1.FileName) + @"\" + Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + i1 + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()).AddMetadata("Link", Path.GetDirectoryName(saveFileDialog1.FileName) + @"\" + Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + i1 + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last());
                                }
                                else 
                                {
                                    sl1ItemGroup.AddItem("EmbeddedResource", Path.GetDirectoryName(saveFileDialog1.FileName) + @"\" + Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + i1 + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last()).AddMetadata("Link", Path.GetDirectoryName(saveFileDialog1.FileName) + @"\" + Path.GetFileName(saveFileDialog1.FileName).Split('.').First() + nextIterationStart + "." + Path.GetFileName(saveFileDialog1.FileName).Split('.').Last());
                                }
                            }
                            auto.InsertAfterChild(sourceitem,auto.LastChild);
                            Source = System.Text.RegularExpressions.Regex.Replace(Source, "Z", Path.GetFileName(openFileDialog1.FileNames[currentphase]));
                            if (!File.Exists(cdir + @"\\Template.cs"))
                            {
                                using (var temp = File.Create(cdir + @"\\Template.cs"))
                                {
                                    var sourcefile = System.Text.Encoding.UTF8.GetBytes(Source);
                                    temp.Write(sourcefile,0,sourcefile.Length);
                                    temp.Flush();
                                    temp.Dispose();
                                }
                            }
                            sourceitem.AddItem("Compile",cdir+@"\\Template.cs");
                            var target = auto.AddTarget("Build");
                            var task = target.AddTask("Csc");
							task.SetParameter("Sources","@(Compile)");
							task.SetParameter("OutputAssembly",cdir+@"\\AutoJoiners\\"+Path.GetFileNameWithoutExtension(openFileDialog1.FileNames[currentphase])+".exe");
                            auto.Save(cdir+@"\\output.csproj");
                            Project program = new Project(auto.DirectoryPath+@"\\output.csproj");
                            program.Build();
                            File.Delete(cdir+@"\\Template.cs");
                            program.ProjectCollection.UnloadAllProjects();
                            File.Delete(auto.DirectoryPath+@"\\output.csproj");
                        }
                            //string[] files = contents.ToArray();
                            /*using (StreamWriter wstream = File.AppendText(Path.GetDirectoryName(saveFileDialog1.FileName) + String.Format(@"\\maps\\" + "{0}.fmap", Path.GetFileName(openFileDialog1.FileName))))
                            {
                                for (int i = 0; i < files.Length; i++)
                                {
                                    byte[] bytes;
                                    if (i != files.Length - 1)
                                    {
                                        bytes = GetBytes(files[i] + Environment.NewLine);
                                        wstream.BaseStream.Write(bytes, 0, bytes.Length);
                                    }
                                    else
                                    {
                                        bytes = GetBytes(files[i]);
                                        wstream.BaseStream.Write(bytes, 0, bytes.Length);
                                    }
                                }
                                wstream.Dispose();
                            }*/
                            //File.AppendAllLines(Path.GetDirectoryName(saveFileDialog1.FileName) + String.Format(@"\\maps\\" + "{0}.fmap", Path.GetFileName(openFileDialog1.FileNames[currentphase])), contents);
                            //File.AppendAllLines(Path.GetDirectoryName(saveFileDialog1.FileName) + String.Format(@"\\maps\\" + "{0}.fmap", Path.GetFileName(openFileDialog1.FileName)), contents, Encoding.ASCII);
                            /*mapwriter.BaseStream.Flush();
                            mapstream.Dispose();
                            mapwriter.Dispose();*/
                            stream1.Dispose();
                    //writer.BaseStream.Flush();
                    //writer.BaseStream.Dispose();
                    writer.Dispose();
                    backgroundWorker1.ReportProgress(fileNumber);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    //}
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (w.IsRunning) 
            { 
                case true:
                    w.Stop();
                    w.Reset();
                    break;
            }
            openFileDialog1.FileNames[currentphase] = null;
            GC.Collect(GC.GetGeneration(openFileDialog1.FileNames[currentphase]),GCCollectionMode.Forced);
            currentphase++;
            if (currentphase < openFileDialog1.FileNames.Length)
            {
                nextIterationStart += Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                byte[] filebytes = File.ReadAllBytes(openFileDialog1.FileNames[currentphase]);
                split = Convert.ToInt32(convertInput(textBox1.Text)) != 0 ? Convert.ToInt32(convertInput(textBox1.Text)) : 1;
                totalfiles = radioButton1.Checked ? filebytes.Length / (filebytes.Length/split):filebytes.Length/split;
                pform.maxvalue = totalfiles;
                backgroundWorker1.RunWorkerAsync();
            }
            else 
            {
                currentphase = 0;
                nextIterationStart = "";
                pform.Text = @"Done";
                MessageBox.Show(@"Task Completed", @"The Files have been Split");
                pform.Dispose();
                Show();
                if (tabhidden) 
                {
                    tabPage2.Show();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        /*private int negate(int number)
        {
            number = number <= (0-1) ? number*-1:number;
            return number;
        }*/
        /*byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }*/
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            w.Stop();
            pform.ProgressText = "Phase:" + (currentphase + 1) + " of " + openFileDialog1.FileNames.Length+Environment.NewLine+"Progress:" + (e.ProgressPercentage) + "/" + (totalfiles) + " files remaining";
            pform.progress = e.ProgressPercentage;
            w.Restart();
        }

        /*public string ByteArrayToString(byte[] ba)
{
   StringBuilder hex = new StringBuilder(ba.Length * 2);
   foreach (byte b in ba)
       hex.AppendFormat("{0:x2}", b);
   return hex.ToString();
}*/

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabPage page = tabControl1.TabPages[e.Index];
            e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(25,32,32,32)), e.Bounds);
            System.Drawing.Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, Font, paddedBounds, System.Drawing.Color.FromArgb(64, 16, 16, 16));
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //byte[] hash = System.IO.File.ReadAllBytes(folderBrowserDialog1.SelectedPath+@"\Hash.sha256");
            IEnumerable<string> files = File.ReadAllLines(filepaths.ElementAt(currentFileSelected));
            int currentitem = 0;
            string file = Path.GetFileName(filepaths.ElementAt(currentFileSelected)).Remove(Path.GetFileName(filepaths.ElementAt(currentFileSelected)).Length - (".fmap").Length);
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(folderBrowserDialog1.SelectedPath + @"\\"+file)))
            {
                foreach (string i in files)
                {
                    //string[] parts = System.IO.Path.GetFileName(i).Split('.');
                    //if (parts[parts.Length - 1].Equals("split")) 
                    //{
                    byte[] bytes = File.ReadAllBytes(i);
                    //int hashindex = 0;
                    //for (int index = 0; index < bytes.Length; index++) 
                    //{
                    //bytes[index] += bytes[hashindex];
                    //hashindex = hashindex + 1 < bytes.Length ? hashindex + 1 : 0;
                    //filebytes.Add(bytes[index]);
                    writer.BaseStream.Write(bytes, 0, bytes.Length);
                    //}
                    backgroundWorker2.ReportProgress(currentitem);
                    currentitem++;
                    //}
                }
                writer.Flush();
                writer.Dispose();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentFileSelected = ((ListBox)sender).SelectedIndex;
            button4.Visible = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) 
            {
                pform = new ProgressUpdate();
                pform.Show();
                pform.maxvalue = (File.ReadAllLines(filepaths.ElementAt(currentFileSelected)).Length);
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pform.ProgressText = "Progress:" + e.ProgressPercentage + "/" + pform.maxvalue;
            pform.progress = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(@"Task Completed", @"The file has been created");
            pform.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(cdir + @"\\maps")) 
            {
                var directoryInfo = Directory.CreateDirectory(cdir+@"\\maps",new DirectorySecurity(cdir+@"\\maps",AccessControlSections.All));
                directoryInfo.Attributes = FileAttributes.Directory|FileAttributes.Hidden;
                tabPage2.Hide();
                tabhidden = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                listBox1.Items.Clear();
                IEnumerable<string> directories = Directory.EnumerateDirectories(cdir);
                var candidates = (from string i in directories
                    where (i.Contains("maps"))
                    select i);
                string store = "";
                for (int index = 0; index < candidates.Count(); index++)
                {
                    if (Directory.EnumerateFiles(candidates.ElementAt(index)).All(s => s.Split('.').Last().Equals("fmap")))
                    {
                        filepaths = (from filename in Directory.EnumerateFiles(candidates.ElementAt(index))
                                     where (filename.Split('.').Last().Equals("fmap"))
                                     select filename);
                        directories = (from filename in Directory.EnumerateFiles(candidates.ElementAt(index))
                                       where (filename.Split('.').Last().Equals("fmap"))
                                       select Path.GetFileName(filename));
                    }
                    store = candidates.ElementAt(index);
                }
                foreach (string i1 in directories)
                {
                    listBox1.Items.Add(i1);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GC.Collect(GC.GetGeneration(label1.Text),GCCollectionMode.Optimized);
            label1.Text = sender.ToString().Contains("True") ? "Enter # of files to split into:":label1.Text;
            label1.Visible = true;
            textBox1.Visible = true;
            button2.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            GC.Collect(GC.GetGeneration(label1.Text), GCCollectionMode.Optimized);
            label1.Text = sender.ToString().Contains("True") ? "Enter bytecount to split at:" : label1.Text;
            label1.Visible = true;
            textBox1.Visible = true;
            button2.Visible = true;
        }
    }
}
