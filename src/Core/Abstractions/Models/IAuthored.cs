using System;

namespace BlogTemplate.Core.Abstractions.Models
{

    /// <summary> Describes an entity that can be maintained by an <see cref="Author"/>. </summary>
    public interface IAuthored
    {

        /// <summary> The unique identifier of the <see cref="Author"/>. </summary>
        Guid AuthorGuid { get; set; }

        /// <summary> A time-stamp of the last time the entity was modified by the <see cref="Author"/>. </summary>
        DateTime LastAuthoredAt { get; set; }

    }

}
