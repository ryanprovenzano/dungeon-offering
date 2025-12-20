using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Sounds", menuName = "Scriptable Objects/Sounds")]

// Proper layout for sound folder is. Assets/Resources/Sound/SoundFolder/soundFiles
public class Sounds : ScriptableObject
{
    public Dictionary<string, Dictionary<string, AudioClip>> soundDictionaries;


    private static AudioClip GetAudioClip(string folderName, string soundFileName)
    {
        // Unity file paths require forward slashes, so Path.Combine wouldn't work
        return Resources.Load<AudioClip>("Sound" + "/" + folderName + "/" + soundFileName);
    }

    void OnEnable()
    {
        soundDictionaries = new();

        // Check if sound directory exists
        string soundDirectoryPath = Path.Combine(Application.dataPath, "Resources", "Sound");
        bool soundDirectoryExists = Directory.Exists(Path.Combine(Application.dataPath, "Resources", "Sound"));

        if (soundDirectoryExists)
        {
            string[] soundFolders = Directory.GetDirectories(soundDirectoryPath);

            // We want separate dictionaries for each folder in Sound 
            foreach (string soundFolder in soundFolders)
            {
                // Get directory name; e.g. "MeleeSFX"
                string soundFolderName = new DirectoryInfo(soundFolder).Name;

                soundDictionaries.Add(soundFolderName, new Dictionary<string, AudioClip>());

                string[] soundFilePaths = Directory.GetFiles(soundFolder);
                foreach (string soundFilePath in soundFilePaths)
                {
                    // We need to skip files that end in .meta
                    if (Path.GetExtension(soundFilePath) == ".meta")
                    {
                        continue;
                    }
                    string fileName = Path.GetFileNameWithoutExtension(soundFilePath);
                    soundDictionaries[soundFolderName].Add(fileName, GetAudioClip(soundFolderName, fileName));
                }


            }
        }
    }
}
