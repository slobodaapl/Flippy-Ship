using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PersistenceController : MonoBehaviour // Simple score loading/saving
{
	public static double bestScore;
	public static bool isLoaded;

	void Awake() // Load once on start, cause static
	{
		if (isLoaded) return;

		isLoaded = true;
		Load();
	}
	
	public static void Save() // Serialize class into data
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fs = File.Create(Application.persistentDataPath + "/ScoreData.dat");
 
		SaveContainer saveData = new SaveContainer();
		saveData.bestScore = bestScore;

		bf.Serialize(fs, saveData);
		fs.Close();
	}
 
	public static void Load() // Load serialized data from save if it exists, and if it's valid
	{
		if (File.Exists(Application.persistentDataPath + "/ScoreData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream fs = File.Open(Application.persistentDataPath + "/ScoreData.dat", FileMode.Open);
			SaveContainer saveData = bf.Deserialize(fs) as SaveContainer;
			fs.Close();
 
			if (saveData != null)
			{
				bestScore = saveData.bestScore;
			}
		}
	}
}
