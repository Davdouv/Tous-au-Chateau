using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVillage : MonoBehaviour {

    [Range(7, 12)]
    public int sizeOfVillageGridUnit;
    public GameObject typeOfHouse1, typeOfHouse2, typeOfHouse3, typeOfHouse4;
    public string genotypeStr;
    public Transform villagePosition;

    private const int nbOfHousePerVillage = 7;
    private GameObject[] typesOfHouses = new GameObject[4];
    private int[] genotype;

    // Use this for initialization
    void Start () {
        /*Initialise le tableau des types de maison*/
        typesOfHouses[0] = typeOfHouse1;
        typesOfHouses[1] = typeOfHouse2;
        typesOfHouses[2] = typeOfHouse3;
        typesOfHouses[3] = typeOfHouse4;

        /*Transforme le genotype de string en tableau d'entier*/
        genotype = new int[genotypeStr.Length];
        for(int i = 0; i < genotypeStr.Length; i++)
        {
            genotype[i] = 0;

            if (genotypeStr[i] == '1')
                genotype[i] = 1;
        }

        /*Génération*/
        DisplayVillage();
    }

    /**************
       Affichage
     *************/

    /*Afficher les villages*/
    private void DisplayVillage()
    {
        GameObject currentVillage = new GameObject("Village");
        //currentVillage.tag = "village";

        GameObject villageGrid = GameObject.CreatePrimitive(PrimitiveType.Plane);
        villageGrid.transform.localScale = new Vector3(sizeOfVillageGridUnit * 9.0f / 10.0f, 1, sizeOfVillageGridUnit * 9.0f / 10.0f); // on divise par 10 car un plane a pour scale 1 = 10 unité unity
        villageGrid.transform.position = new Vector3(villageGrid.transform.localScale.x * 10 / 2.0f, 0, villageGrid.transform.localScale.z * 10 / 2.0f);
        villageGrid.transform.parent = currentVillage.transform;

        DisplayHouses(currentVillage);

        currentVillage.transform.position = villagePosition.position;

    }

    /*Afficher les maisons d'un village*/
    private void DisplayHouses(GameObject parentVillage)
    {
        for (int i = 0; i < nbOfHousePerVillage; ++i)
        {
            //lecture genotype
            int type = genotype[i * 11] * 2 + genotype[i * 11 + 1];
            int angleRotation = genotype[i * 11 + 2] * 4 + genotype[i * 11 + 3] * 2 + genotype[i * 11 + 4];
            angleRotation *= 45; //on ne veut que des valeurs tous les 45 degrés
            int posX = genotype[i * 11 + 5] * 4 + genotype[i * 11 + 6] * 2 + genotype[i * 11 + 7];
            int posY = genotype[i * 11 + 8] * 4 + genotype[i * 11 + 9] * 2 + genotype[i * 11 + 10];

            //création de l'objet
            float houseHeight = 2;
            Mesh mesh = typesOfHouses[type].GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
            if (mesh != null)
            {
                houseHeight = mesh.bounds.size.y / 2;
            }

            GameObject currentHouse = Instantiate(typesOfHouses[type]);
            currentHouse.transform.rotation = Quaternion.AngleAxis(angleRotation, Vector3.up);
            currentHouse.transform.position = new Vector3(posX * sizeOfVillageGridUnit + sizeOfVillageGridUnit, houseHeight, posY * sizeOfVillageGridUnit + sizeOfVillageGridUnit);
            currentHouse.transform.parent = parentVillage.transform;
        }
    }
}
