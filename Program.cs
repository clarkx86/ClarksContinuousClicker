using System;
using System.Collections.Generic;
using System.Reflection;
using Mono.Options;

namespace ClarksContinuousClicker
{
    class Program
    {
        private static bool _verbose;
        public static Version Version = Assembly.GetExecutingAssembly().GetName().Version;
        static void Main(string[] args)
        {
            Console.WriteLine("{0} v{1}.{2}.{3} by {4}\n", "Clarks Continuous Clicker", Version.Major, Version.Minor, Version.Revision, "clarkx86");

            bool printHelp = false;

            VirtualMouse vMouse = new VirtualMouse();

            OptionSet options = new OptionSet()
                .Add("h|help", "Display an overview of available parameters.", (string h) => { printHelp = h != null; } )
                .Add("v", "Whether to enable verbosity.", (string v) => { _verbose = v != null; } )
                .Add("b=|mouse-button=", "Mouse button to virtually click.", (string value) =>
                {
                    switch (value.ToLower())
                    {
                        case "left":
                            vMouse.Button = MouseButton.LEFT;
                            break;

                        case "middle":
                            vMouse.Button = MouseButton.MIDDLE;
                            break;

                        case "right":
                            vMouse.Button = MouseButton.RIGHT;
                            break;

                        default:
                            Console.WriteLine("\"{0}\" is an invalid option for a mouse-button. Use --help to get a overview of valid options.", value);
                            System.Environment.Exit(0);
                            break;
                    }
                })
                .Add("i=|interval=", "Interval to press mouse button in milliseconds.", (uint value) => { vMouse.Interval = value; })
                .Add("x:", "(Optional) X coordinate to place mouse cursor.", (uint x) => { vMouse.Position.X = (int)x; })
                .Add("y:", "(Optional) Y coordinate to place mouse cursor.", (uint y) => { vMouse.Position.Y = (int)y; });
            List<string> extraOptions = options.Parse(args);

            if (printHelp)
            {
                options.WriteOptionDescriptions(Console.Out);
                System.Environment.Exit(0);
            }

            if (_verbose)
            {
                vMouse.OnAction += (object sender, EventArgs e) =>
                {
                    Console.WriteLine("Mouse event triggered: {0}", Enum.GetName(typeof(MouseButton), vMouse.Button));
                };
            }

            if (vMouse.Interval == null)
            {
                System.Console.WriteLine("Interval must be greater than 0.");
            }

            vMouse.Enable();

            Console.WriteLine("Virtual mouse enabled:\n\tButton:\t\t{0}\n\tInterval:\t{1}\n\nPress \"ENTER\" to toggle virtual mouse or \"ESCAPE\" to quit.\n", Enum.GetName(typeof(MouseButton), vMouse.Button), vMouse.Interval);

            while (vMouse.Enabled)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            vMouse.Disable();
                            Console.WriteLine("Virtual mouse paused.");

                            bool result = false;
                            while (!result)
                            {
                                result = Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter ? true : false;
                            }

                            vMouse.Enable();
                            Console.WriteLine("Virtual Mouse resumed.");
                            break;

                        case ConsoleKey.Escape:
                            vMouse.Disable();
                            break;
                    }
                }
            }
        }
    }
}
