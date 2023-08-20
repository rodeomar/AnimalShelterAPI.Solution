namespace AnimalShelterAPI.Models
{
    public class AnimalData
    {
        public List<Cat> Cats { get; set; }
        public List<Dog> Dogs { get; set; }

        public void Filter(string animal, int? id, string? breed, int? age, bool? isAdopted)
        {
         
            if (animal.ToLower() == "both")
            {
                Filter(Cats, id, breed, age, isAdopted);
                Filter(Dogs, id, breed, age, isAdopted);
            }else if (animal.ToLower() == "cat")
            {
                Filter(Cats, id, breed, age, isAdopted);
            }else if(animal.ToLower() == "dog")
            {
                Filter(Dogs, id, breed, age, isAdopted);
            }

        }

        public void Filter<T>(List<T> data, int? id, string? breed, int? age, bool? isAdopted)
        {
            if (id != null)
            {
                data.RemoveAll(item => GetId(item) != id);
            }

            if (!string.IsNullOrEmpty(breed))
            {
                data.RemoveAll(item => GetBreed(item) != breed);
            }

            if (age != null)
            {
                data.RemoveAll(item => GetAge(item) != age);
            }

            if (isAdopted != null)
            {
                data.RemoveAll(item => GetIsAdopted(item) != isAdopted);
            }
            
        }

        private int GetId<T>(T item)
        {
            if (item is Cat cat)
            {
                return cat.Id;
            }
            else if (item is Dog dog)
            {
                return dog.Id;
            }

            return -1;
        }

        private string GetBreed<T>(T item)
        {
            if (item is Cat cat)
            {
                return cat.Breed;
            }
            else if (item is Dog dog)
            {
                return dog.Breed;
            }

            return string.Empty;
        }

        private int GetAge<T>(T item)
        {
            if (item is Cat cat)
            {
                return cat.Age;
            }
            else if (item is Dog dog)
            {
                return dog.Age;
            }

            return 0;
        }

        private bool GetIsAdopted<T>(T item)
        {
            if (item is Cat cat)
            {
                return cat.IsAdopted;
            }
            else if (item is Dog dog)
            {
                return dog.IsAdopted;
            }

            return false;
        }

    }

}
