com.pixelwizards.packageutil
=========================

[![openupm](https://img.shields.io/npm/v/com.pixelwizards.package-utils?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pixelwizards.package-utils/)

Package Utilities

Usage
--------------

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.pixelwizards.package-utils
```

### Install via git url

Add this to your project manifest.json

```
"com.pixelwizards.package-utils": "https://github.com/PixelWizards/com.pixelwizards.package-utils.git",
```

OpenUPM Support
----------------

This package is also available via the OpenUPM scoped registry: 
https://openupm.com/packages/com.pixelwizards.package-utils/

You can find the via Window->General->Package Utilities

Prerequistes
---------------
* This has been tested for `>= 2018.3`

Known Issues
---------------
- json serialization for Dependencies isn't correct currently. (Have to figure out how to make
a custom newtonsoft serializer for the PackageDependency type)

Content
----------------

### Tools

* Window/General/Package Utilities

### Samples

* None currently

Required dependencies
---------------
* None 
