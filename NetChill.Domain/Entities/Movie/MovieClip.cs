using Microsoft.AspNetCore.Http;
using NetChill.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetChill.Domain.Entities.Movie
{
    public class MovieClip : BaseAuditableEntity
    {
        public int ClipId { get; set; }
        public string MoviePosterPath { get; set; } //"^(?:(?<scheme>[^:\/?#]+):)?(?:\/\/(?<authority>[^\/?#]*))?(?<path>[^?#]*\/)?(?<file>[^?#]*\.(?<extension>[Jj][Pp][Ee]?[Gg]|[Pp][Nn][Gg]))(?:\?(?<query>[^#]*))?(?:#(?<fragment>.*))?$"gm
        public string VideoClipPath { get; set; } // "(?i:^.*\.(mp4|webm|Ogg)$)"gm
        public DateTime UploadDate { get; set; }
        public Guid MovieRef {  get; set; }
        public MovieBaseInfo MovieBaseInfo { get; set; }
    }
}
