﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clockwise;
using WorkspaceServer.Packaging;

namespace WorkspaceServer.Tests.Packaging
{
    public class FakeToolPackageLocator : IToolPackageLocator
    {
        private readonly InMemoryDirectoryAccessor _directory;

        public FakeToolPackageLocator(InMemoryDirectoryAccessor directory)
        {
            _directory = directory;
        }

        public async Task<DirectoryInfo> PrepareToolAndLocateAssetDirectory(FileInfo tool, Budget budget = null)
        {
            // e.g. C:\Users\abc\.trydotnet\packages\.store\PKGNAME\1.0.0\PKGNAME\1.0.0\tools\netcoreapp2.1\any\project

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(tool.Name);

            var relativeToolDllPath = _directory.GetAllDirectoriesRecursively()
                                                .SingleOrDefault(d =>
                                                {
                                                    if (d.Value.EndsWith($"/runner-{fileNameWithoutExtension}/"))
                                                    {
                                                        return true;
                                                    }

                                                    return false;
                                                });

            if (relativeToolDllPath != null)
            {
                return (DirectoryInfo) _directory.GetFullyQualifiedPath(relativeToolDllPath);
            }

            return null;
        }
    }
}