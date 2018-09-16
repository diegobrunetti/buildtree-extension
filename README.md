# buildtree-extension
Extension method that builds up a parent/child data structure from a IEnumerable&lt;T>.

## usage:

This extension adds a `BuildTree()` method to IEnumerable&lt;T>. For more info on how to use it, see _samples_ folder.


1. Install the nuget package:

**Package Manager:** 
```
Install-Package BuildTreeExtension
```

**.NET CLI:** 
```
dotnet add package BuildTreeExtension
```


1. Given a data structure like this

```csharp
var list = new List<Category>
{
    new Category
    {
        Id = "1",
        Description = "Computers"
    },
    new Category
    {
        Id = "2",
        ParentId = "1",
        Description = "Notebooks"
    },
    new Category
    {
        Id = "3",
        ParentId = "2",
        Description = "Notebook's subcategory"
    },
    new Category
    {
        Id = "4",
        Description = "Books"
    },
    new Category
    {
        Id = "5",
        ParentId = "4",
        Description = "Biographies"
    }
};
```

1. Making a call to `BuildTree()` method

```csharp
var tree = list.BuildTree(k => k.Id, g => g.ParentId);
```

will return a tree data structure like this:

```json
[
  {
    "Id": "1",
    "Current": {
      "Id": "1",
      "Description": "Computers",
      "ParentId": null
    },
    "Children": [
      {
        "Id": "2",
        "Current": {
          "Id": "2",
          "Description": "Notebooks",
          "ParentId": "1"
        },
        "Children": [
          {
            "Id": "3",
            "Current": {
              "Id": "3",
              "Description": "Notebook's subcategory",
              "ParentId": "2"
            },
            "Children": []
          }
        ]
      }
    ]
  },
  {
    "Id": "4",
    "Current": {
      "Id": "4",
      "Description": "Books",
      "ParentId": null
    },
    "Children": [
      {
        "Id": "5",
        "Current": {
          "Id": "5",
          "Description": "Biographies",
          "ParentId": "4"
        },
        "Children": []
      }
    ]
  }
]
```