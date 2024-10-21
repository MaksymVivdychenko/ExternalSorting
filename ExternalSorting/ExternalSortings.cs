namespace ExternalSorting
{
    public class DirectMergingStandart
    {
        private string SourceFile { get; set; }
        private string DestinationFile { get; set; }
        private string helpBFile;
        private string helpCFile;
        private long sizeOfFile;
        public DirectMergingStandart(string sourceFile, string destinationFile, int sizeOfFileinMB)
        {
            SourceFile = sourceFile;
            DestinationFile = destinationFile;
            File.Copy(sourceFile, DestinationFile, true);
            helpBFile = "B.txt";
            helpCFile = "C.txt";
            sizeOfFile = ((long)sizeOfFileinMB)* 1024 * 128;
        }
        public void Sorting()
        {
            for (long i = 1; i < sizeOfFile; i *= 2)
            {
                SplitFile(i);
                UnionFiles(i);
            }
            File.Delete(helpBFile);
            File.Delete(helpCFile);
        }
        private void SplitFile(long sizeOfSegment)
        {
            using (StreamWriter streamWriterB = new(helpBFile))
            using (StreamWriter streamWriterC = new(helpCFile))
            using (StreamReader streamReaderA = new(DestinationFile))
            {
                while (streamReaderA.EndOfStream == false)
                {
                    for (int j = 0; j < sizeOfSegment && streamReaderA.EndOfStream == false; j++)
                    {
                        streamWriterB.WriteLine(streamReaderA.ReadLine());
                    }
                    for (int j = 0; j < sizeOfSegment && streamReaderA.EndOfStream == false; j++)
                    {
                        streamWriterC.WriteLine(streamReaderA.ReadLine());
                    }
                }
            }
        }
        private void UnionFiles(long sizeOfSegment)
        {
            string b = "", c = "";
            long bInt, cInt, bCounter = 0, cCounter = 0;
            using (StreamReader streamReaderB = new(helpBFile))
            using (StreamReader streamReaderC = new(helpCFile))
            using (StreamWriter streamWriterA = new(DestinationFile))
            {
                b = streamReaderB.ReadLine();
                c = streamReaderC.ReadLine();
                while (b != null && c != null)
                {
                    bCounter = 0; cCounter = 0;
                    while (bCounter < sizeOfSegment && cCounter < sizeOfSegment && b != null && c != null)
                    {
                        bInt = long.Parse(b);
                        cInt = long.Parse(c);
                        if (CompareTwoNums(bInt, cInt))
                        {
                            streamWriterA.WriteLine(cInt);
                            cCounter++;
                            c = streamReaderC.ReadLine();
                        }
                        else
                        {
                            streamWriterA.WriteLine(bInt);
                            bCounter++;
                            b = streamReaderB.ReadLine();
                        }
                    }
                    if (bCounter < sizeOfSegment)
                    {
                        while (b != null && bCounter < sizeOfSegment)
                        {
                            streamWriterA.WriteLine(b);
                            b = streamReaderB.ReadLine();
                            bCounter++;
                        }
                    }
                    else
                    {
                        while (c != null && cCounter < sizeOfSegment)
                        {
                            streamWriterA.WriteLine(c);
                            c = streamReaderC.ReadLine();
                            cCounter++;
                        }
                    }
                }
                if (b == null)
                {
                    while (c != null)
                    {
                        streamWriterA.WriteLine(c);
                        c = streamReaderC.ReadLine();
                    }
                }
                else if (c == null)
                {
                    while (b != null)
                    {
                        streamWriterA.WriteLine(b);
                        b = streamReaderB.ReadLine();
                    }
                }
            }

        }

        private bool CompareTwoNums(long a, long b)
        {
            if (a >= b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class DirectMergingModified
    {
        private string SourceFile { get; set; }
        private const long sizeOfMassForSorting = 1310720;
        private string DestinationFile { get; set; }
        private string helpBFile;
        private string helpCFile;
        private long sizeOfFile;
        public DirectMergingModified(string sourceFile, string destinationFile, int sizeOfFileinMB)
        {
            SourceFile = sourceFile;
            DestinationFile = destinationFile;
            //File.Copy(sourceFile, DestinationFile, true);
            helpBFile = "B.txt";
            helpCFile = "C.txt";
            sizeOfFile = ((long)sizeOfFileinMB) * 1024 * 128;
        }
        public void Sorting()
        {
            PreventionSorting();
            for (long i = sizeOfMassForSorting; i < sizeOfFile; i *= 2)
            {
                SplitFile(i);
                UnionFiles(i);
            }
            File.Delete(helpBFile);
            File.Delete(helpCFile);
        }
        public void PreventionSorting()
        {
            using (StreamWriter streamWriter = new StreamWriter(DestinationFile))
            using (StreamReader streamReader = new StreamReader(SourceFile))
            {
                List<long> massForSorting = new List<long>();
                while (streamReader.EndOfStream == false)
                {
                    for (long i = 0; i < sizeOfMassForSorting && streamReader.EndOfStream == false; i++)
                    {
                        massForSorting.Add(long.Parse(streamReader.ReadLine()));
                    }
                    massForSorting.Sort();
                    foreach (long i in massForSorting)
                    {
                        streamWriter.WriteLine(i);
                    }
                }

            }
        }
        private void SplitFile(long sizeOfSegment)
        {
            using (StreamWriter streamWriterB = new(helpBFile))
            using (StreamWriter streamWriterC = new(helpCFile))
            using (StreamReader streamReaderA = new(DestinationFile))
            {
                while (streamReaderA.EndOfStream == false)
                {
                    for (int j = 0; j < sizeOfSegment && streamReaderA.EndOfStream == false; j++)
                    {
                        streamWriterB.WriteLine(streamReaderA.ReadLine());
                    }
                    for (int j = 0; j < sizeOfSegment && streamReaderA.EndOfStream == false; j++)
                    {
                        streamWriterC.WriteLine(streamReaderA.ReadLine());
                    }
                }
            }
        }
        private void UnionFiles(long sizeOfSegment)
        {
            string b = "", c = "";
            long bInt, cInt, bCounter = 0, cCounter = 0;
            using (StreamReader streamReaderB = new(helpBFile))
            using (StreamReader streamReaderC = new(helpCFile))
            using (StreamWriter streamWriterA = new(DestinationFile))
            {
                b = streamReaderB.ReadLine();
                c = streamReaderC.ReadLine();
                while (b != null && c != null)
                {
                    bCounter = 0; cCounter = 0;
                    while (bCounter < sizeOfSegment && cCounter < sizeOfSegment && b != null && c != null)
                    {
                        bInt = long.Parse(b);
                        cInt = long.Parse(c);
                        if (CompareTwoNums(bInt, cInt))
                        {
                            streamWriterA.WriteLine(cInt);
                            cCounter++;
                            c = streamReaderC.ReadLine();
                        }
                        else
                        {
                            streamWriterA.WriteLine(bInt);
                            bCounter++;
                            b = streamReaderB.ReadLine();
                        }
                    }
                    if (bCounter < sizeOfSegment)
                    {
                        while (b != null && bCounter < sizeOfSegment)
                        {
                            streamWriterA.WriteLine(b);
                            b = streamReaderB.ReadLine();
                            bCounter++;
                        }
                    }
                    else
                    {
                        while (c != null && cCounter < sizeOfSegment)
                        {
                            streamWriterA.WriteLine(c);
                            c = streamReaderC.ReadLine();
                            cCounter++;
                        }
                    }
                }
                if (b == null)
                {
                    while (c != null)
                    {
                        streamWriterA.WriteLine(c);
                        c = streamReaderC.ReadLine();
                    }
                }
                else if (c == null)
                {
                    while (b != null)
                    {
                        streamWriterA.WriteLine(b);
                        b = streamReaderB.ReadLine();
                    }
                }
            }

        }

        private bool CompareTwoNums(long a, long b)
        {
            if (a >= b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
