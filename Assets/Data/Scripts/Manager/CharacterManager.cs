using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CharacterManager : MonoBehaviour
    {
        private static Dictionary <string, CharacterObject> allcharacters;
        public List<CharacterObject> characterObjects;
        static CharacterManager current;

        void Awake()
        {
            current = this;
            allcharacters = new Dictionary<string, CharacterObject>();
            foreach(var characterObject in characterObjects)
            {
                allcharacters.Add(characterObject.CharacterName, characterObject);
            }
        }

        public static CharacterObject FindCharacter(string character)
        {   
            string result = current.ParseName(character);
            // print(result);
            if(allcharacters.ContainsKey(result))
            {
                return allcharacters[result];
            }

            return null;
        }

        private string ParseName(string character)
        {   
            string outputString = character;

            // Find all instances of "#name#" in the input string
            int startIndex = character.IndexOf("#");
            int endIndex = character.IndexOf("#", startIndex + 1);

            while (startIndex != -1 && endIndex != -1)
            {
                // Extract the name from between the "#" symbols
                string name = character.Substring(startIndex + 1, endIndex - startIndex - 1);

                // Remove the "#" symbols and capitalize the first character of the name
                string formattedName = char.ToUpper(name[0]) + name.Substring(1);

                // Replace the placeholder with the formatted name in the output string
                outputString = outputString.Replace("#" + name + "#", formattedName);

                // Find the next occurrence of "#name#" in the input string
                startIndex = character.IndexOf("#", endIndex + 1);
                endIndex = character.IndexOf("#", startIndex + 1);
            }
            return outputString;
        }
    }
}