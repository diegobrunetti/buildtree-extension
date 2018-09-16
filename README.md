# buildtree-extension
Extension method that builds up a parent/child data structure from a IEnumerable&lt;T>.

## usage:

This extension adds a `BuildTree()` method to IEnumerable<T>. For more info on how to use it, see _samples_ folder.


1. Add a reference to _System.Collections.Generic_;

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
    ,
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
list.BuildTree(k => k.Id, g => g.ParentId);
```

will give you a tree data structure like this: