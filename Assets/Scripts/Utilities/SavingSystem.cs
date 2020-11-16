using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/*
	https://gamedev.stackexchange.com/questions/80638/binary-serialization-of-c-for-gameobject-in-unity
*/


public static class SavingSystem
{
	public static void Serialize< T >( string path, T data )
	{
		CreateFolderIfNotExists( path );

		FileStream fs				= File.Create( path );
		BinaryFormatter bf			= new BinaryFormatter();

		bf.Serialize(fs, data);

		// https://answers.unity.com/questions/1035204/serializing-to-disk-sometimes-fails-on-android-but.html
		fs.Close();
		fs.Dispose();
	}


	public static T Deserialize< T >( string path ) where T : class
	{
		if (!File.Exists( path ))
			throw new FileNotFoundException( "File not found", path );

		using (FileStream fs		= File.Open( path, FileMode.Open ))
		{
			BinaryFormatter bf		= new BinaryFormatter();
			T data					= (T)bf.Deserialize( fs );

			return data;
		}
	}


	static void CreateFolderIfNotExists( string path )
	{
		string folder		= Path.GetDirectoryName( path );

		if (!Directory.Exists( folder ))
			Directory.CreateDirectory( folder );
	}


	public static string DefaultPath( string fileName )
	=>
		Path.Combine( Application.persistentDataPath, fileName );
}

