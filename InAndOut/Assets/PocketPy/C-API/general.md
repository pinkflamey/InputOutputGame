---
title: General
icon: dot
order: 7
---
#### `void pkpy_delete(void* p)`

Delete a pointer allocated by `pkpy_xxx_xxx`.
It can be `VM*`, `REPL*`, `char*`, etc.

!!!
If the pointer is not allocated by `pkpy_xxx_xxx`, the behavior is undefined.
!!!

#### `void pkpy_setup_callbacks(f_int_t f_int, f_float_t f_float, f_bool_t f_bool, f_str_t f_str, f_None_t f_None)`

Setup the callback functions.