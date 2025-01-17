﻿using System;
using System.Collections.Generic;

namespace ChildrenVillageSOS_DAL.Models;

public partial class Image
{
    public int Id { get; set; }

    public string? UrlPath { get; set; }

    public string? ChildId { get; set; }

    public string? HouseId { get; set; }

    public string? VillageId { get; set; }

    public int? EventId { get; set; }

    public string? UserAccountId { get; set; }

    public int? ActivityId { get; set; }

    public int? InventoryId { get; set; }

    public string? SchoolId { get; set; }

    public int? HealthReportId { get; set; }

    public int? AcademicReportId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AcademicReport? AcademicReport { get; set; }

    public virtual Activity? Activity { get; set; }

    public virtual Child? Child { get; set; }

    public virtual Event? Event { get; set; }

    public virtual HealthReport? HealthReport { get; set; }

    public virtual House? House { get; set; }

    public virtual Inventory? Inventory { get; set; }

    public virtual School? School { get; set; }

    public virtual UserAccount? UserAccount { get; set; }

    public virtual Village? Village { get; set; }
}
