﻿using balsa.archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace balsa.setup {
    public class AssetManager {
        public List<FileContainer> containers;
        public readonly Game game;
        internal Type archiveFileType;

        public AssetManager(Game game) {
            this.game = game;
            var prefix = $"balsa.archives.{game.abbreviation}";
            archiveFileType = Type.GetType($"{prefix}ArchiveFile");
        }

        public ArchiveFile LoadArchive(string filePath) {
            var fileName = Path.GetFileName(filePath);
            ArchiveFile archive = (ArchiveFile) Activator.CreateInstance(
                archiveFileType, new object[1] { fileName }
            );
            new ArchiveFileSource(filePath, archive);
            archive.ReadHeader();
            archive.ReadBody();
            containers.Add(archive);
            return archive;
        }

        public List<string> GetLoadedContainers() {
            return containers.Select(fc => fc.path).ToList();
        }

        public void LoadFolder(string path) {
            // ?
        }

        public void BuildArchive(List<string> filePaths) {
            // ?
        }

        public byte[] GetTextureData(string filePath) {
            // ?
            return null;
        }
    }
}