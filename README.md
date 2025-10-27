# Practica-05SI

# Creacion de Escenario
Este es el **ejercisio** que realizamos en clase de prácticas donde generamos el **escenario base del Cardboard** tras realizar toda la **configuración necesaria**.

# Moviento Escenario y Recoleccion

El **jugador** puede interactuar con el **entorno** solo con la **mirada**, sin necesidad de **botones**.  
Se utilizan **tres scripts principales** que trabajan en conjunto: **GazeTeleportToPoint**, **GazeCollecSword** y **SwordObserver2**.

El primero permite **teletransportarse** a puntos del mapa, mientras que los otros dos controlan la **recolección de espadas** mediante **eventos**.

Para el **GazeTeleportToPoint**, este permite que el **jugador** se **teletransporte** mirando puntos del escenario que tienen la **etiqueta "point"**.  
La **cámara del jugador** lanza un **raycast** hacia adelante, y si detecta uno de estos puntos dentro de la **capa "Interactive"**, empieza a contar un **temporizador**.  Si el **jugador** mantiene la **mirada** durante unos **segundos (gazeTime)**, el script mueve al **jugador** directamente hasta la **posición del punto mirado**. Si deja de mirar, el **temporizador** se reinicia.

El script de **GazeCollectSword** actúa como un **emisor de eventos** para la recolección de espadas.  Cuando el **jugador** mantiene la **mirada** sobre un objeto con la **etiqueta "Espada"** durante el tiempo establecido en **gazeTime**, el script lanza un **evento (OnRecolectarEspada)** enviando como referencia el **objeto espada** y la **posición del jugador**.  

Si el **jugador** deja de mirar el objeto antes de completar el tiempo, el **temporizador (timer)** se reinicia.  
Esto asegura que solo se recojan las espadas cuando la mirada se mantiene de manera intencional.

Cada espada tiene su propio script **SwordObserver2**, que está **suscrito al evento** lanzado por **GazeCollectSword**.  
Cuando se activa la recolección mediante **OnSwordCollected**, la espada verifica si es la que fue mirado por el **jugador**.  
Si es así, la espada comienza a **moverse automáticamente** hacia el **jugador**  y desactiva su **Rigidbody** para evitar que la física interfiera con el movimiento.  
Una vez que la espada llega lo suficientemente cerca del jugador, se **desactiva** para simular que ha sido **recolectada**.


# Objeto recolector
El script de **GazeRecolectar** actúa como un **emisor de eventos**.  
Cuando el **jugador** mira un objeto con la **etiqueta "recolectar"** durante el tiempo establecido, el script lanza un **evento (OnRecolectarEspadas)** y envía la **posición del jugador** como referencia.

Cada espada tiene su propio script **SwordObserver**, que está **suscrito al evento** lanzado por **GazeRecolectar**.  
Cuando se activa la **recolección**, las **espadas** comienzan a **moverse automáticamente** hacia el **jugador** utilizando una **interpolación suave**.  
Una vez que llegan lo suficientemente cerca, se **desactivan** para simular que han sido **recogidas**.



