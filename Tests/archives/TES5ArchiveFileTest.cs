﻿using NUnit.Framework;
using balsa.setup;
using balsa.archives;
using balsa;
using System.Linq;
using System.Text;

namespace Tests.archives {
    public class TES5ArchiveFileTest {
        public AssetManager manager;
        public TES5ArchiveFile archive;

        [OneTimeSetUp]
        public void Setup() {
            manager = new AssetManager(Games.TES5);
            var archivePath = TestHelpers.FixturePath("TES5.bsa");
            archive = (TES5ArchiveFile) manager.LoadArchive(archivePath);
        }

        [Test]
        public void TestArchiveHeader() {
            byte[] bsaMagic = Encoding.ASCII.GetBytes("BSA\0");
            Assert.IsTrue(archive.header.fileId.SequenceEqual(bsaMagic));
            Assert.AreEqual(104, archive.header.version);
            Assert.AreEqual(1, archive.header.folderCount);
            Assert.AreEqual(1, archive.header.fileCount);
            Assert.AreEqual(10, archive.header.totalFolderNameLength);
            Assert.AreEqual(9, archive.header.totalFileNameLength);
            Assert.AreEqual(256, archive.header.fileFlags);
        }

        [Test]
        public void TestArchiveFlags() {
            var flags = archive.header.archiveFlags;
            Assert.IsTrue(flags.HasFlag("Include Directory Names"));
            Assert.IsTrue(flags.HasFlag("Include File Names"));
            Assert.IsFalse(flags.HasFlag("Compressed"));
            Assert.IsFalse(flags.HasFlag("Retain Directory Names"));
            Assert.IsFalse(flags.HasFlag("Retain File Names"));
            Assert.IsFalse(flags.HasFlag("Retain File Name Offsets"));
            Assert.IsFalse(flags.HasFlag("Xbox 360 Archive"));
            Assert.IsFalse(flags.HasFlag("Retain Strings During Startup"));
            Assert.IsFalse(flags.HasFlag("Embed File Names"));
            Assert.IsFalse(flags.HasFlag("XMem Codec"));
        }
    }
}
