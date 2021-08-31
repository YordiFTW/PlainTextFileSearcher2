using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PlainTextFileSearcher.Business
{
    public class SearchResultDataService
    {
        public static List<String> SearchForTextinDocumentsFromSelectedFolder(string startFolder, string searchTerm)
        {
            List<String> list = new List<String>();

            // Modify this path as necessary.  
            startFolder = @"C:\Users\Yordi\Documents\plain-text-file-searcher-opdracht\articles";

            // Take a snapshot of the file system.  
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            foreach(var file in fileList)
            {
                System.IO.File.ReadAllLines(file.ToString());
            }

            searchTerm = @"1";

            // Search the contents of each file.  
            // A regular expression created with the RegEx class  
            // could be used instead of the Contains method.  
            // queryMatchingFiles is an IEnumerable<string>.  
            var queryMatchingFiles =
                from file in fileList
                where file.Extension == ".html"
                let fileText = GetFileText(file.FullName)
                where fileText.Contains(searchTerm)
                select file.FullName;

            // Execute the query.  
            //Console.WriteLine("The term \"{0}\" was found in:", searchTerm);
            foreach (string filename in queryMatchingFiles)
            {
                //Console.WriteLine(filename);
                list.Add(filename);
            }

            return list;
            // Keep the console window open in debug mode.  
            //Console.WriteLine("Press any key to exit");
            //Console.ReadKey();
        }

        // Read the contents of the file.  

        public static (string , int , int)SearchForTextinDocumentsFromSelectedFile(string searchTerm, string startFolder)
        { 

            List<string> list = new List<string>();
            var content = "";
            int filesWithMatches = 0;
            int totalMatches = 0;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            Parallel.ForEach(fileList, file =>
            {
                bool repeat = false;
                string[] textFile = File.ReadAllLines(file.ToString());
                for (int i = 0; i < textFile.Length; i++)
                {
                    string line = textFile[i];
                    int charactersBeforeSearchTerm;
                    int charactersAfterSearchTerm;
                    int searchTermIndex;
                    string folderName;


                    if (line.Contains(searchTerm))
                    {
                        totalMatches += 1;
                        searchTermIndex = line.IndexOf(searchTerm);

                        if (searchTermIndex - 20 <= 0)
                        {
                            charactersBeforeSearchTerm = 0;
                        }
                        else
                        {
                            charactersBeforeSearchTerm = searchTermIndex - 20;
                        }

                        if (searchTermIndex + searchTerm.Length + 20 <= line.Length)
                        {
                            charactersAfterSearchTerm = searchTerm.Length + 20;
                            if (charactersBeforeSearchTerm > 0) charactersAfterSearchTerm += 20;
                        }
                        else
                        {
                            charactersAfterSearchTerm = line.Length - searchTermIndex;
                        }


                        folderName = file.FullName.Replace(startFolder, "");

                        //list.Add(line);

                        //content += Environment.NewLine + fileName + Environment.NewLine + line.Substring(charactersBeforeSearchTerm, charactersAfterSearchTerm);
                        if (repeat == false)
                        {
                            content += Environment.NewLine + Environment.NewLine + folderName;
                            filesWithMatches += 1;
                            repeat = true;
                            
                        }
         
                            content += Environment.NewLine + (i + 1)+ Environment.NewLine + line.Substring(charactersBeforeSearchTerm, charactersAfterSearchTerm);

                        //list.Add(line.Substring(charactersBeforeSearchTerm, charactersAfterSearchTerm - charactersBeforeSearchTerm));
                        //content += line.Substring(charactersBeforeSearchTerm,
                        //    charactersAfterSearchTerm - charactersBeforeSearchTerm);
                        //           content += Environment.NewLine;
                    }
                };
            });

            //string[] textFile = File.ReadAllLines(@"C:\Users\Yordi\source\repos\PlainTextFileSearcher2\articles\4\0\0\400_meter_horden.html");


            



            //foreach (string line in textFile)
            //{
            //    if (line.Contains(searchTerm))
            //    {
            //        int charactersBeforeSearchTerm;
            //        int charactersAfterSearchTerm;
            //        int searchTermIndex;



            //        searchTermIndex = line.IndexOf(searchTerm);

            //        if(searchTermIndex - 20 <= 0)
            //        {
            //            charactersBeforeSearchTerm = 0;
            //        }
            //        else
            //        {
            //            charactersBeforeSearchTerm = searchTermIndex - 20;
            //        }

            //        if (searchTermIndex + searchTerm.Length + 20 <= line.Length)
            //        {
            //            charactersAfterSearchTerm = searchTermIndex + searchTerm.Length + 20;
            //        }
            //        else
            //        {
            //            charactersAfterSearchTerm = line.Length;
            //        }



            //        //list.Add(line);

            //        list.Add(line.Substring(charactersBeforeSearchTerm, charactersAfterSearchTerm - charactersBeforeSearchTerm ));



            //    }
            //}

            return (content, totalMatches, filesWithMatches); 
        }
        static string GetFileText(string name)
        {
            string fileContents = String.Empty;

            // If the file has been deleted since we took
            // the snapshot, ignore it and return the empty string.  
            if (System.IO.File.Exists(name))
            {
                fileContents = System.IO.File.ReadAllText(name);
            }
            return fileContents;
        }

        


    }
}

