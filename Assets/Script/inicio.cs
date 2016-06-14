﻿using UnityEngine;
using System.Collections;

public class inicio : MonoBehaviour {
	public Texture BoxTexture;
	public Texture cuadroTexture;
	public Texture pj1Texture;
	public Texture pj2Texture;
	public Texture pj3Texture;
	public GameObject pj1,pj2,pj3, objetoInstanciar;
	private string username = General.username, nickname="", caracteristicas = "";
	private bool continuar = false, tienePersonaje = false, correcto = false;

	// Use this for initialization
	void Start () {
		string url = General.hosting+"consultarPersonaje";
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		WWW www = new WWW(url, form);
		StartCoroutine(consultarSitienePersonaje(www));

		string url2 = General.hosting+"consultarPersonajeId";
		WWWForm form2 = new WWWForm();
		form2.AddField("username", General.username);
		WWW www2 = new WWW(url2, form2);
		StartCoroutine(General.consultarPersonajeUsername(www2));
		System.Threading.Thread.Sleep(2000);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 8,(Screen.height/16), Screen.width / 6, Screen.height/4), pj1Texture)) {
			General.idPersonaje = 1;
			General.personaje = pj1;
			GameObject otro = GameObject.FindGameObjectWithTag ("Player");
			Destroy (otro);
			GameObject personaje = Instantiate (General.personaje, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
			personaje.GetComponent<movimiento>().enabled = false;
			string url = General.hosting+"consultarPersonajeCaracteristicas";
			WWWForm form = new WWWForm();
			form.AddField("id", 1);
			WWW www = new WWW(url, form);
			StartCoroutine(consultarPersonaje(www));
		}

		if (GUI.Button (new Rect (Screen.width / 8,5*(Screen.height/16), Screen.width / 6, Screen.height/4), pj2Texture)) {
			General.idPersonaje = 2;
			General.personaje = pj2;
			GameObject otro = GameObject.FindGameObjectWithTag ("Player");
			Destroy (otro);
			GameObject personaje = Instantiate (General.personaje, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
			personaje.GetComponent<movimiento>().enabled = false;
			string url = General.hosting+"consultarPersonajeCaracteristicas";
			WWWForm form = new WWWForm();
			form.AddField("id", 2);
			WWW www = new WWW(url, form);
			StartCoroutine(consultarPersonaje(www));
		}

		if (GUI.Button (new Rect (Screen.width / 8,9*(Screen.height/16), Screen.width / 6, Screen.height/4), pj3Texture)) {
			General.idPersonaje = 3;
			General.personaje = pj3;
			GameObject otro = GameObject.FindGameObjectWithTag ("Player");
			Destroy (otro);
			GameObject personaje = Instantiate (General.personaje, objetoInstanciar.transform.position, objetoInstanciar.transform.rotation) as GameObject;
			personaje.GetComponent<movimiento>().enabled = false;

			string url = General.hosting+"consultarPersonajeCaracteristicas";
			WWWForm form = new WWWForm();
			form.AddField("id", 3);
			WWW www = new WWW(url, form);
			StartCoroutine(consultarPersonaje(www));
			System.Threading.Thread.Sleep(1000);
		}

		if (tienePersonaje || correcto)
		{
			if(General.idPersonaje == 1)
			{
				General.personaje = pj1;
			}else if (General.idPersonaje == 2)
			{
				General.personaje = pj2;
			}else if(General.idPersonaje == 3)
			{
				General.personaje = pj3;
			}

			continuar = true;
			if(General.personaje != null)
				Application.LoadLevel ("level1");
		}
		if(!continuar)
		{
			GUIStyle style = new GUIStyle();
			style.alignment = TextAnchor.MiddleCenter;
			GUI.color = Color.red;
			GUI.Label(new Rect(5*(Screen.width/8), Screen.height/10, 3*(Screen.width/8), Screen.height/2),"Caracteristicas");
			GUI.Label(new Rect(5*(Screen.width/8), Screen.height/6, 5*(Screen.width/8), Screen.height/2),cuadroTexture);

			GUI.color = Color.white;
			GUI.Label(new Rect(5*(Screen.width/8), Screen.height/6, 5*(Screen.width/8), Screen.height/2),caracteristicas);

			GUI.color = Color.black;
			GUI.Label(new Rect(2*(Screen.width/8), 5 *(Screen.height/6), Screen.width/6, Screen.height/10),"Alias:");
			GUI.color = Color.white;
			nickname = GUI.TextField(new Rect(3*(Screen.width / 8), 5 *(Screen.height/6), Screen.width / 4, Screen.height/12),nickname,25);

			if (GUI.Button (new Rect (5*(Screen.width / 8), 5 *(Screen.height/6), Screen.width / 5, Screen.height/12), "Crear")) {
				if(validarPersonaje())
				{
					string url = General.hosting+"crearPersonaje";
					WWWForm form = new WWWForm();
					form.AddField("username", General.username);
					form.AddField("id", General.idPersonaje);
					form.AddField("nickname", nickname);
					WWW www = new WWW(url, form);
					StartCoroutine(crearPersonaje(www));
				}
			}
		}else{
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), BoxTexture);
			GUI.Label(new Rect(Screen.width - Screen.width/4 , Screen.height-Screen.height/6, Screen.width/4, Screen.height/6),"Cargando...");
		}
	}

	public IEnumerator consultarSitienePersonaje(WWW www){
		yield return www;
		if(www.error == null){

			if (int.Parse(www.text) ==  1) {
				tienePersonaje = true;
			} else {
				Debug.Log (www.text);
			}

		}else{
			Debug.Log(www.error);
		}
	}

	public IEnumerator crearPersonajeUser(WWW www){
		yield return www;
		if(www.error == null){
			
			if (int.Parse(www.text) ==  1) {
				tienePersonaje = true;
			} else {
				Debug.Log (www.text);
			}
			
		}else{
			Debug.Log(www.error);
		}
	}

	public IEnumerator consultarPersonaje(WWW www){
		yield return www;
		if(www.error == null){
			Debug.Log(www.text);
			caracteristicas = www.text;
		}else{
			Debug.Log(www.error);
		}
	}

	private IEnumerator crearPersonaje(WWW www){
		yield return www;
		if(www.error == null){
			Debug.Log(www.text);
			correcto = true;
		}else{
			Debug.Log(www.error);
		}
	}

	private bool validarPersonaje()
	{
		if (General.idPersonaje == 0)
			return false;
		else if (nickname == "")
			return false;
		else
			return true;
	}
}