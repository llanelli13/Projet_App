using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

// Définition de la classe Personne et des classes dérivées
public abstract class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}