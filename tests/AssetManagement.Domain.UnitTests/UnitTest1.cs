using AssetManagement.Domain.Assets;
using AssetManagement.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AssetManagement.Domain.UnitTests;

public class FieldSetTests
{
    [Fact]
    public void Create_WithValidFields_ShouldInitializeItemsCorrectly()
    {
        // Arrange
        var name = Name.Create("Laptop Specs").Value;
        var customField1 = CustomField.Create("RAM", CustomFieldType.Text).Value;
        var customField2 = CustomField.Create("CPU", CustomFieldType.Text).Value;
        
        var fields = new List<CustomField> { customField1, customField2 };

        // Act
        var fieldSetResult = FieldSet.Create(name, fields);

        // Assert
        Assert.True(fieldSetResult.IsSuccess);
        var fieldSet = fieldSetResult.Value;
        
        Assert.Equal(2, fieldSet.Items.Count);
        Assert.Equal(customField1.Id, fieldSet.Items.ElementAt(0).CustomFieldId);
        Assert.Equal(1, fieldSet.Items.ElementAt(0).Order);
        
        Assert.Equal(customField2.Id, fieldSet.Items.ElementAt(1).CustomFieldId);
        Assert.Equal(2, fieldSet.Items.ElementAt(1).Order);
    }
}
