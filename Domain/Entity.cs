﻿namespace IWantApp.Domain;

public abstract class Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }

}
