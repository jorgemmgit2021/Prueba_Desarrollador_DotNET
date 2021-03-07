export const navigation = [
  {
    text: 'Inicio',
    path: '/home',
    icon: 'home'
  },
  {
    text: 'Movimientos',
    icon: 'folder',
    items: [
      {
        text: 'Ventas',
        path: '/ventas'
      },
      {
        text: 'Totales de ventas',
        path: '/movimientos'
      }
    ]
  },
  {
    text:'Clientes',
    icon:'folder',
    items:[
      {text:'Consulta historial',path:'/historial'},
      {text:'Registros de ventas',path:'/registro'},
    ]
  },
  {
    text:'Inventarios',
    icon:'folder',
    items:[
      {text:'Listado',path:'/listado'},
      {text:'Items en stock',path:'/items'}
    ]
  },
  {text:'Catalogos',icon:'folder'}
];
