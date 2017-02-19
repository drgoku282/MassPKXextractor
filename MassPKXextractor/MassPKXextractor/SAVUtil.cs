/// I do not own the code in this class. 
/// All rights and credits for the code in this class belong to Kaphotics.
/// All code within this class is taken from PKHeX https://github.com/kwsch/PKHeX
/// with minor modifications
/// 

using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MassPKXextractor
{
    public static class SAVUtil
    {
        public static bool getSavesFromFolder(string folderPath, bool deep, out IEnumerable<string> result)
        {
            if (!Directory.Exists(folderPath))
            {
                result = null;
                return false;
            }
            try
            {
                var searchOption = deep ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var files = Directory.GetFiles(folderPath, "*", searchOption);
                result = files.Where(f => SizeValidSAV((int)new FileInfo(f).Length));
                return true;
            }
            catch (ArgumentException)
            {
                result = new[] {"Error encountered when detecting saves in the following folder:" + Environment.NewLine + folderPath,
                    "Advise manually scanning to remove bad filenames from the folder." + Environment.NewLine + "Likely caused via Homebrew creating invalid filenames."};
                return false;
            }
        }

        public static bool SizeValidSAV(int size)
        {
            switch (size)
            {
                // Gen 1
                case SaveUtil.SIZE_G1RAW:
                case SaveUtil.SIZE_G1BAT:
                // Gen 2
                // case SaveUtil.SIZE_G2RAW_J: Same as SIZE_G3RAWHALF
                // case SaveUtil.SIZE_G2RAW_U: Same as SIZE_G1RAW
                case SaveUtil.SIZE_G2BAT_J:
                // case SaveUtil.SIZE_G2BAT_U: Same as SIZE_G1BAT
                case SaveUtil.SIZE_G2EMU:
                case SaveUtil.SIZE_G2VC:
                // Gen 3
                case SaveUtil.SIZE_G3RAW:
                case SaveUtil.SIZE_G3RAWHALF:
                // Gen 4
                case SaveUtil.SIZE_G4RAW:
                case SaveUtil.SIZE_G4RAW + 0x7A:
                // Gen 5
                // case SaveUtil.SIZE_G4RAW: Same as SIZE_G4RAW
                // Gen 6
                case SaveUtil.SIZE_G6XY:
                case SaveUtil.SIZE_G6ORASDEMO:
                case SaveUtil.SIZE_G6ORAS:
                // Gen 7
                case SaveUtil.SIZE_G7SM:
                // Colosseum
                case SaveUtil.SIZE_G3COLO:
                case SaveUtil.SIZE_G3COLOGCI:
                // XD
                case SaveUtil.SIZE_G3XD:
                case SaveUtil.SIZE_G3XDGCI:
                // Box RS
                // case SaveUtil.SIZE_G3BOX: SIZE_G6ORAS
                case SaveUtil.SIZE_G3BOXGCI:
                // Battle Revolution
                case SaveUtil.SIZE_G4BR:
                    return true;
                default:
                    return false;
            }
        }

        public static bool dumpBoxes(this SaveFile SAV, string path, out string result, bool boxFolders = false)
        {
            PKM[] boxdata = SAV.BoxData;
            if (boxdata == null)
            { result = "Invalid Box Data, unable to dump."; return false; }

            int ctr = 0;
            foreach (PKM pk in boxdata)
            {
                if (pk.Species == 0 || !pk.Valid)
                    continue;

                ctr++;
                string fileName = Util.CleanFileName(pk.FileName);
                string boxfolder = "";
                if (boxFolders)
                {
                    boxfolder = SAV.getBoxName(pk.Box);
                    Directory.CreateDirectory(Path.Combine(path, boxfolder));
                }
                if (!File.Exists(Path.Combine(Path.Combine(path, boxfolder), fileName)))
                    File.WriteAllBytes(Path.Combine(Path.Combine(path, boxfolder), fileName), pk.DecryptedBoxData);
            }

            result = $"Dumped Boxes ({ctr} pkm) to path:\n" + path;
            return true;
        }
    }
}
