﻿using System.IO;
using Microsoft.DotNet.Try.Markdown;
using WorkspaceServer.Servers.Roslyn;

namespace WorkspaceServer
{
    public static class DirectoryAccessor
    {
        public static bool DirectoryExists(
            this IDirectoryAccessor directoryAccessor,
            string relativePath) =>
            directoryAccessor.DirectoryExists(new RelativeDirectoryPath(relativePath));

        public static void EnsureDirectoryExists(
            this IDirectoryAccessor directoryAccessor,
            string relativePath) =>
            directoryAccessor.EnsureDirectoryExists(new RelativeDirectoryPath(relativePath));

        public static bool FileExists(
            this IDirectoryAccessor directoryAccessor,
            string relativePath) =>
            directoryAccessor.FileExists(new RelativeFilePath(relativePath));

        public static string ReadAllText(
            this IDirectoryAccessor directoryAccessor,
            string relativePath) =>
            directoryAccessor.ReadAllText(new RelativeFilePath(relativePath));

        public static void WriteAllText(
            this IDirectoryAccessor directoryAccessor,
            string relativePath,
            string text) =>
            directoryAccessor.WriteAllText(
                new RelativeFilePath(relativePath),
                text);

        public static IDirectoryAccessor GetDirectoryAccessorForRelativePath(
            this IDirectoryAccessor directoryAccessor,
            string relativePath) =>
            directoryAccessor.GetDirectoryAccessorForRelativePath(new RelativeDirectoryPath(relativePath));

        public static IDirectoryAccessor GetDirectoryAccessorFor(this IDirectoryAccessor directory, DirectoryInfo directoryInfo)
        {
            var relative = PathUtilities.GetRelativePath(
                directory.GetFullyQualifiedRoot().FullName, 
                directoryInfo.FullName);
            return directory.GetDirectoryAccessorForRelativePath(new RelativeDirectoryPath(relative));
        }

        public static DirectoryInfo GetFullyQualifiedRoot(this IDirectoryAccessor directoryAccessor) =>
            (DirectoryInfo) directoryAccessor.GetFullyQualifiedPath(new RelativeDirectoryPath("."));

        public static FileInfo GetFullyQualifiedFilePath(this IDirectoryAccessor directoryAccessor, string relativeFilePath) =>
            (FileInfo) directoryAccessor.GetFullyQualifiedPath(new RelativeFilePath(relativeFilePath));

    }
}