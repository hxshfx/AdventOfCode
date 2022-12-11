using CoreAoC.Entities;
using System.Runtime.CompilerServices;
using static AdventOfCode.Problems.Y2021.P5;

[assembly: InternalsVisibleTo("TestingProject")]
namespace AdventOfCode.Problems.Y2022
{
    internal class P7 : Problem
    {
        internal class P7_1 : Part
        {
            private const int _MAX_DIR_SIZE = 100000;


            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines.Skip(1).ToList()).ToString();


            private static long ComputeNonRecursive(IList<string> lines)
            {
                DirectoryTree dt = new("/");
                dt.BuildTree(lines);
                
                return ToDeleteSum(dt);
            }

            private static long ToDeleteSum(DirectoryTree dt)
                => ToDeleteSum(dt.SubDirectories);

            private static long ToDeleteSum(IDictionary<string, DirectoryTree> subDirectories)
            {
                long result = 0;

                foreach (DirectoryTree dt in subDirectories.Select(kvp => kvp.Value).Where(dt => GetDirectorySize(dt) < _MAX_DIR_SIZE))
                    result += GetDirectorySize(dt);
                foreach (DirectoryTree dt in subDirectories.Select(kvp => kvp.Value))
                    result += ToDeleteSum(dt.SubDirectories);

                return result;
            }
        }   

        internal class P7_2 : Part
        {
            private const long _MAX_FSYS_SIZE = 70000000;
            private const long _REQUIRED_SIZE = 30000000;


            protected override object Compute(IEnumerable<string> lines)
                => ComputeNonRecursive(lines.Skip(1).ToList()).ToString();


            private static long ComputeNonRecursive(IList<string> lines)
            {
                DirectoryTree dt = new("/");
                dt.BuildTree(lines);

                IDictionary<string, long> directorySizes = MinToDelete(dt);
                long spaceToFree = _REQUIRED_SIZE - (_MAX_FSYS_SIZE - GetDirectorySize(dt));

                return directorySizes
                    .Where(kvp => kvp.Value >= spaceToFree)
                    .OrderBy(kvp => kvp.Value)
                    .First().Value;
            }


            private static IDictionary<string, long> MinToDelete(DirectoryTree dt)
                => MinToDelete(dt.SubDirectories);


            private static IDictionary<string, long> MinToDelete(IDictionary<string, DirectoryTree> subDirectories)
            {
                IDictionary<string, long> directorySizes = new Dictionary<string, long>();

                foreach (KeyValuePair<string, DirectoryTree> kvp in subDirectories)
                    directorySizes.Add(kvp.Key, GetDirectorySize(kvp.Value));
                foreach (DirectoryTree dt in subDirectories.Select(d => d.Value))
                    directorySizes = MergeDicts(directorySizes, MinToDelete(dt.SubDirectories));

                return directorySizes;
            }

            private static IDictionary<string, long> MergeDicts(IDictionary<string, long> dictA, IDictionary<string, long> dictB)
            {
                foreach (var item in dictB)
                {
                    if (dictA.ContainsKey(item.Key))
                        dictA[item.Key] = item.Value;
                    else
                        dictA.Add(item.Key, item.Value);
                }

                return dictA;
            }
        }


        private sealed class DirectoryTree
        {
            public string Cwd { get; set; }
            public string Root { get; }

            public IDictionary<string, long> FileSizes { get; }
            public IDictionary<string, DirectoryTree> SubDirectories { get; }


            public DirectoryTree(string root)
            {
                Cwd = root;
                Root = root;

                FileSizes = new Dictionary<string, long>();
                SubDirectories = new Dictionary<string, DirectoryTree>();
            }


            public void BuildTree(IList<string> lines)
            {
                DirectoryTree dt = this;

                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith("$ cd"))
                        Cwd = NewCwd(Cwd, Root, lines[i]);
                    else if (lines[i].Equals("$ ls"))
                        i = ListCommand(dt, lines, i);
                }

                Cwd = NewCwd(Cwd, Root, Root);
            }


            private void AddDirectory(string path)
            {
                if (!Cwd.Equals(Root))
                    Navigate(this, path).AddDirectory(path);
                else
                    SubDirectories.Add(path, new DirectoryTree(path));
            }

            private void AddFile(string path, long fileSize)
            {
                if (!Cwd.Equals(Root))
                    Navigate(this, path).AddFile(path, fileSize);
                else
                    FileSizes.Add(path, fileSize);
            }


            private static DirectoryTree GetSubDirectory(IDictionary<string, DirectoryTree> subDirectories, string path)
                => subDirectories[path];

            private static string NewCwd(string cwd, string root, string path)
            {
                string result;

                if (path.Equals("$ cd /"))
                    result = root;
                else if (path.Equals("$ cd .."))
                    result = string.Join('/', cwd.Split('/').SkipLast(2)) is string str &&
                        !string.IsNullOrEmpty(str) ? $"{str}/" : "/";
                else
                    result = $"{cwd}{path.Split(' ').Last()}/";

                return result;
            }

            private static int ListCommand(DirectoryTree dt, IList<string> lines, int i)
            {
                while (++i < lines.Count && !lines[i].StartsWith('$'))
                {
                    if (lines[i].StartsWith("dir "))
                        dt.AddDirectory(lines[i].Split("dir ").Last());
                    else
                    {
                        long fileSize = long.Parse(lines[i].Split(' ').First());
                        string fileName = lines[i].Split(' ').Last();

                        dt.AddFile(fileName, fileSize);
                    }
                }

                if (i != lines.Count) i--;
                return i;
            }

            private static DirectoryTree Navigate(DirectoryTree dt, string path)
            {
                foreach (string subPath in $"{dt.Cwd}{path}".Split('/').Skip(1).SkipLast(1))
                {
                    if (!string.IsNullOrEmpty(subPath))
                        dt = GetSubDirectory(dt.SubDirectories, subPath);
                }

                return dt;
            }
        }

        private static long GetDirectorySize(DirectoryTree dt)
                => dt.FileSizes.Values.Sum() + dt.SubDirectories.Sum(d => GetDirectorySize(d.Value));
    }
}
