using CoreAoC.Entities;

namespace CoreAoC.Utils
{
    internal static class FileTextExplorer
    {
        public static IDictionary<int, IEnumerable<FileInfo>> GetInputInfo(string inputsPath)
        {
            IDictionary<int, IEnumerable<FileInfo>> result = new Dictionary<int, IEnumerable<FileInfo>>();

            foreach (DirectoryInfo di in Directory.GetDirectories(inputsPath).Select(d => new DirectoryInfo(d)))
                result.Add(int.Parse(di.Name[1..]), di.GetFiles());

            return result;
        }

        public static IDictionary<Problem, IEnumerable<string>> ReadData(IEnumerable<FileInfo> files)
        {
            IDictionary<Problem, IEnumerable<string>> result = new Dictionary<Problem, IEnumerable<string>>();

            IEnumerable<Type> problemTypes = AssemblySearcher.GetProblemsFromYear(int.Parse(files.First().Directory!.Name[1..]));
            foreach (FileInfo fi in files)
            {
                string problemName = fi.Name.Replace(fi.Extension, string.Empty);
                Problem problem = (Problem)Activator.CreateInstance(problemTypes.Single(t => t.Name.Equals(problemName)))!;

                result.Add(problem, File.ReadAllLines(fi.FullName));
            }

            return result;
        }
    }
}
