# Proyecto
### Proyecto de Estructuras de Datos I de la Universidad Rafael Landivar
> ##### El proyecto del curso de Estructura de Datos I consistió en una aplicación que funcionara como un servicio de transmisión de contenido (documentales, series, peliculas) vía Internet. Por lo que se desarrolló una aplicación haciendo uso de C# con un estilo de Modelo Vista Controlador (MVC) que almacenara los datos de cada contenido digital en una estructura de datos no lineal específica: arbol B, por ser un arbol balanceado de búsqueda lo cual permitiera el fácil acceso a un dato específico.
> ### Arbol B
> ##### En las ciencias de la computación, los árboles-B o B-árboles son estructuras de datos de árbol que se encuentran comúnmente en las implementaciones de bases de datos y sistemas de archivos. Al igual que los árboles binarios de búsqueda, son árboles balanceados de búsqueda, pero cada nodo puede poseer más de dos hijos.Los árboles B mantienen los datos ordenados y las inserciones y eliminaciones se realizan en tiempo logarítmico amortizado.
> ##### La idea tras los árboles-B es que los nodos internos deben tener un número variable de nodos hijo dentro de un rango predefinido. Cuando se inserta o se elimina un dato de la estructura, la cantidad de nodos hijo varía dentro de un nodo. Para que siga manteniéndose el número de nodos dentro del rango predefinido, los nodos internos se juntan o se parten. Dado que se permite un rango variable de nodos hijo, los árboles-B no necesitan rebalancearse tan frecuentemente como los árboles binarios de búsqueda auto-balanceables. Pero, por otro lado, pueden desperdiciar memoria, porque los nodos no permanecen totalmente ocupados. Los límites (uno superior y otro inferior) en el número de nodos hijo son definidos para cada implementación en particular. Por ejemplo, en un árbol-B 2-3 (A menudo simplemente llamado árbol 2-3 ), cada nodo sólo puede tener 2 o 3 nodos hijo.

Un árbol-B se mantiene balanceado porque requiere que todos los nodos hoja se encuentren a la misma altura.
