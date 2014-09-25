using Zbu.ModelsBuilder;

//Don't generate the folder media type, because it only has the Folder Browser property.
[assembly:IgnoreContentType("Folder")]

//Just an example to demo that the generated classes can also have other names than the document types.
//[assembly: RenameContentType("Website", "Site")]