using System;

namespace Framework.Core.Abstractions;

public interface IEvent
{
    public DateTime? PublishedOn { get; set; }
}
