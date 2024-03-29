﻿using Flunt.Validations;


namespace IWantApp.Domain.Products;

public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; set; }


    public Category(string name, string createBy, string editedBy)
    {

        Name = name;
        Active = true;
        CreateBy = createBy;
        EditedBy = editedBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }
    private void Validate()
    {
        var contract = new Contract<Category>()
        .IsNotNullOrEmpty(Name, "Name")
        .IsGreaterOrEqualsThan(Name, 3, "Name")
        .IsNotNullOrEmpty(CreateBy, "CreatedBy")
        .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }

    public void EditInfo(string name, bool active)
    {
        Active = active;
        Name = name;
        Validate();
    }

}
