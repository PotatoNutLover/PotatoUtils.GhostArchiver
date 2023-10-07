using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.App
{
    [Serializable]
    public class Configuration
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FileExtension
        {
            get; set;
        }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string FolderPath
        {
            get; set;
        }

        [Range(0, Int32.MaxValue - 1)]
        public int MinFileSize
        {
            get; set;
        }

        [Required]
        [Range(5, 1000)]
        public int AttemptCount
        {
            get; set;
        }

        [Required]
        [Range(0.1f, 10)]
        public float AttemptDelay
        {
            get; set;
        }

        public override string ToString()
        {
            string output =
                $"File extension:    {FileExtension} \r\n" +
                $"Folder path:       {FolderPath} \r\n" +
                $"Mininal file size: {MinFileSize} bytes \r\n" +
                $"Attempt count:     {AttemptCount} \r\n" +
                $"Attempt delay:     {AttemptDelay} \r\n";

            return output;
        }
    }
}
