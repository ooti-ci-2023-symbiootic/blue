using System.Formats.Asn1;

class UserData {
    public string Name {get; set;}
    public List<Questions> Questions {get; set;}
}

class Questions {
 public string Type {get; set;}
 public string Options {get; set;}
 public string Answer { get; set;}

}