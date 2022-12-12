using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class EnemyBossSpider : MonoBehaviour
{
    public Character player;
    public LifeController lifeController;
    [SerializeField] private int[] aristas_origen = { };
    [SerializeField] private int[] aristas_destino = { };
    [SerializeField] private int[] aristas_pesos = { };
    private int nodoIni;
    private int nodoFin;
    [SerializeField] private Nodes[] nodesArray;
    [SerializeField] private Aristas[] numsArray;
    [SerializeField] private Dictionary<int, Nodes> nodesDiccionary = new Dictionary<int, Nodes>();
    [SerializeField] private Dictionary<int, Aristas> aristasDiccionary = new Dictionary<int, Aristas>();
    private GrafoMA grafoEst = new GrafoMA();
    [SerializeField] private List<int> currentsNumber = new List<int>();
    [SerializeField] private List<int> vueltaDenums = new List<int>();
    private string number;
    private int nextPoint;
    private int pointInitial=1;
    [SerializeField] private float distanceToWaypoint;
    [SerializeField] private float speed;
    public int nodoselect;

    public int[] vertices = { };
    public int[] numsAristas = { };
    private void Awake()
    {
        lifeController.InitializateLife();
        nextPoint = pointInitial;
    }
    private void Start()
    {
        Main();
        ConseguirInt();
    }

    private void Update()
    {
        MuestroResultadosAlg("Resultados", AlgDijkstra.distance, nodoselect, grafoEst.Etiqs, AlgDijkstra.nodos);
        ConseguirInt();
        MoveEnemy();
        CheckDead();
    }
    void Main()
    {
        // inicializo TDA
        grafoEst.InicializarGrafo();

        for (int i = 0; i < nodesArray.Length; i++)
        {
            nodesArray[i].id = vertices[i];
            nodesDiccionary.Add(nodesArray[i].id, nodesArray[i]);
        }

        for (int i = 0; i < aristas_pesos.Length; i++)
        {
            numsArray[i].id = numsAristas[i];
            numsArray[i].idOrigen = aristas_origen[i];
            numsArray[i].idDestino = aristas_destino[i];
            numsArray[i].idPeso = aristas_pesos[i];
            aristasDiccionary.Add(numsArray[i].id, numsArray[i]);
        }
        foreach (KeyValuePair<int, Nodes> nodo in nodesDiccionary)
        {
            grafoEst.AgregarVertice(nodo.Key);
        }
        foreach (KeyValuePair<int, Aristas> arista in aristasDiccionary)
        {
            grafoEst.AgregarArista(arista.Key, arista.Value.idOrigen, arista.Value.idDestino, arista.Value.idPeso);
        }

        for (int i = 0; i < grafoEst.cantNodos; i++)
        {
            Nodes n1 = null;
            if (nodesDiccionary.TryGetValue(grafoEst.Etiqs[i], out n1))
            {
                //Debug.Log(n1.name);
            }

            for (int j = 0; j < grafoEst.cantNodos; j++)
            {
                if (grafoEst.MAdy[i, j] != 0)
                {
                    Aristas a1 = null;
                    if (aristasDiccionary.TryGetValue(grafoEst.Etiqs[j], out a1))
                    {
                        // obtengo la etiqueta del nodo origen, que est� en las filas (i)
                        nodoIni = grafoEst.Etiqs[i];

                        // obtengo la etiqueta del nodo destino, que est� en las columnas (j)
                        nodoFin = grafoEst.Etiqs[j];
                    }
                }
            }
        }

        // PASO EL GRAFO Y EL COMIENZO
        AlgDijkstra.Dijkstra(grafoEst, 1);
        // muestro resultados
        MuestroResultadosAlg("Resultados", AlgDijkstra.distance, grafoEst.cantNodos, grafoEst.Etiqs, AlgDijkstra.nodos);
    }

    public void MuestroResultadosAlg(string name, int[] distance, int verticesCount, int[] Etiqs, string[] caminos)
    {
        int distancia = 0;
        //maximo valor es hasta donde llega el recorrido (verticesCount)
        for (int i = 0; i < verticesCount; ++i)
        {
            Nodes n1 = null;
            if (distance[i] == int.MaxValue)
            {
                distancia = 0;
            }
            else
            {
                //distancia es peso de aristas
                distancia = distance[i];
            }
            number = caminos[i];
        }

        
    }
    private void ConseguirInt()
    {
        // PASAJE DE STRING A INT
        for (int i = 0; i < number.Length; i++)
        {
            int tmp = 0;
            int.TryParse(number[i].ToString(), out tmp);
            currentsNumber.Add(tmp);
        }
        for (int i = currentsNumber.Count; i==0  ; i--)
        {
            vueltaDenums.Add(i);
        }
    }

    public void CheckDead()
    {
        if (lifeController.currentLife <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }
    private void MoveEnemy()
    {
        // ME MUEVO A TRAVES DE LOS NODOS
        foreach (KeyValuePair<int, Nodes> nodo in nodesDiccionary)
        {
            if (nextPoint == nodo.Key)
            {
                transform.position = Vector3.MoveTowards(transform.position, nodo.Value.transform.position, speed*Time.deltaTime);

                if (Vector3.Distance(transform.position, nodo.Value.transform.position) < distanceToWaypoint)
                {
                    nodo.Value.ChangeColor();
                    nextPoint++;

                    if (!currentsNumber.Contains(nextPoint))
                    {
                        nextPoint++;
                    }
                }
            }
            else if (!currentsNumber.Contains(nextPoint))
            {
                nextPoint++;

                if (nextPoint > currentsNumber.Count)
                {
                    currentsNumber.Clear();
                    nodoselect = 1;
                    nodo.Value.BackColorDefault();
                    nextPoint = pointInitial;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.lifeController.GetDamage(20);
        }
    }
}