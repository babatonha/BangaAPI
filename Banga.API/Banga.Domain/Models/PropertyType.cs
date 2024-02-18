using System;
using System.Collections.Generic;

namespace Banga.Data.Models;

public partial class PropertyType
{
    public int PropertyTypeId { get; set; }

    public string Name { get; set; } = null!;
}
