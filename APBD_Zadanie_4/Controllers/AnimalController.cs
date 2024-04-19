using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

// In SDK-style projects such as this one, several assembly attributes that were historically
// defined in this file are now automatically added during build and populated with
// values defined in project properties. For details of which attributes are included
// and how to customise this process see: https://aka.ms/assembly-info-properties


// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("a414ed72-56e8-4a0d-b615-7abacf03735a")]

namespace APBD_Zadanie_4.Controllers
{
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        AnimalsDataStore _AnimalsDataStore;
        public AnimalsController()
        {
            _AnimalsDataStore = AnimalsDataStore.Current;
        }

        [HttpGet]
        [Route("api/animals")]
        public ActionResult<IEnumerable<Animal>> GetAnimals()
        {
            return Ok(_AnimalsDataStore.Animals);
        }

        [HttpGet]
        [Route("api/animal")]
        public ActionResult<IEnumerable<Animal>> GetAnimal(int id)
        {
            try
            {
                return Ok(_AnimalsDataStore.Animals.Where(a => a.Id == id).First());
            }catch (Exception ex)
            {
                return BadRequest("Wrong id");
            }
        }

        [HttpPut]
        [Route("api/animal")]
        public ActionResult<IEnumerable<Animal>> AddAnimal(string Category, string Name, double Weight, string Color)
        {
            int id = _AnimalsDataStore.Animals.MaxBy(a => a.Id).Id+1;
            Animal toAdd = new Animal();
            toAdd.Id = id;
            toAdd.Category = Category;
            toAdd.Name = Name;
            toAdd.Weight = Weight;
            toAdd.Color = Color;

            _AnimalsDataStore.Animals.Add(toAdd);

            return Ok(toAdd);
        }

        [HttpPost]
        [Route("api/animal")]
        public ActionResult<IEnumerable<Animal>> EditAnimal(int id, string? Category, string? Name, double? Weight, string? Color)
        {
            var index = _AnimalsDataStore.Animals.FindIndex(c => c.Id == id);

            if(Category != null)
                _AnimalsDataStore.Animals[index].Category = Category;

            if(Name != null)
                _AnimalsDataStore.Animals[index].Name = Name;
           
            if(Weight != null)
                _AnimalsDataStore.Animals[index].Weight = (double)Weight;

            if(Color != null)
                _AnimalsDataStore.Animals[index].Color = Color;

           

            return Ok(_AnimalsDataStore.Animals);
        }

        [HttpDelete]
        [Route("api/animals")]
        public ActionResult<IEnumerable<Animal>> DeleteAnimals(int id)
        {
            _AnimalsDataStore.Animals.RemoveAll(a => a.Id == id);
            return Ok(_AnimalsDataStore.Animals);
        }
    }
}