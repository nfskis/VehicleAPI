# VehicleAPI

The solutions should used framework:
  - .Net 5.0


# Basic usage

ColReorder is initialised using the `colReorder` option in the DataTables constructor - a simple boolean `true` will enable the feature. Further options can be specified using this option as an object - see the documentation for details.

Example:

```js
$(document).ready( function () {
    $('#myTable').DataTable( {
    	colReorder: true
    } );
} );
```