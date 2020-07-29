//using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
public class HUD1 : MonoBehaviour
{

	#region Atributos

	[SerializeField] GameObject[] botones;
	[SerializeField] int posicion;
	[SerializeField] string nombre;
	[SerializeField] BotonNombre tipo;

	#endregion

	#region Metodos Privados

	void Awake()
	{
		botones = GameObject.FindGameObjectsWithTag("Boton");
		posicion = 0;
		nombre = null;
	}

	void Update()
	{

		if (Input.GetKeyDown(KeyCode.UpArrow) && posicion < botones.Length - 1)
		{
			posicion++;
			nombre = botones[posicion].name;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) && posicion > 0)
		{
			posicion--;
			nombre = botones[posicion].name;
		}
		// ejecutamos dicho boton
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			nombre = botones[posicion].name;
			BotonNombre tipo = (BotonNombre) System.Enum.Parse(typeof(BotonNombre), nombre);
			switch (tipo)
			{
				case BotonNombre.Ayuda:
					Ayuda();
					break;

				case BotonNombre.Configuracion:
					Configuracion();
					break;

				case BotonNombre.Inicio:
					Inicio();
					break;

				case BotonNombre.Jugar:
					Jugar();
					break;

				case BotonNombre.Niveles:
					Niveles();
					break;

				case BotonNombre.Pausa:
					Pausa();
					break;

				case BotonNombre.Salir:
					Salir();
					break;
			}
		}
	}
	#endregion

	#region Metodos Publicos

	public void Ayuda()
	{
		SceneManager.LoadScene("HelpMenu");
	}

	public void Configuracion()
	{
		SceneManager.LoadScene("MenuConfiguracion");
	}

	public void Inicio()
	{
		SceneManager.LoadScene("MenuInicio");
	}

	public void Jugar()
	{
		SceneManager.LoadScene("Gameplay");
	}

	public void Niveles()
	{
		SceneManager.LoadScene("MenuNiveles");
	}

	public void Pausa()
	{
		Instantiate(Resources.Load<GameObject>("MenuPause"));
	}

	public void Salir()
	{
		Application.Quit();
	}

	#endregion
}