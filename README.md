# TopshelfWithQuartz

## Feature
- **Topshelf** to create a console application that can be installed as Windows service
- **Quartz.NET** for scheduling system
- **Serilog** for logging on a rolling basis
- Remoting server using TCP and client architecture
- **Autofac IoC** (using *Topshelf.Autofac* and *Autofac.Extras.Quartz*)

## Package
```shell
Install-Package Quartz
Install-Package Serilog
Install-Package Serilog.Sinks.Console
Install-Package Serilog.Sinks.File
Install-Package Topshelf
Install-Package Topshelf.serilog
Install-Package Topshelf.Autofac
Install-Package Autofac.Extras.Quartz
```