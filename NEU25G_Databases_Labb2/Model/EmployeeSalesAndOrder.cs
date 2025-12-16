using System;
using System.Collections.Generic;

namespace NEU25G_Databases_Labb2;

public partial class EmployeeSalesAndOrder
{
    public int AnställdsId { get; set; }

    public string Namn { get; set; } = null!;

    public string? Butik { get; set; }

    public int? AntalFörsäljningar { get; set; }

    public int? AntalSåldaArtiklar { get; set; }

    public decimal? FörsäljningsbeloppIKr { get; set; }

    public int? AntalBeställdaOrdrar { get; set; }

    public int? AntalSkickadeOrdrar { get; set; }

    public int? AntalMottagnaOrdrar { get; set; }
}
