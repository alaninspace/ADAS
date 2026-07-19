using MudBlazor;
// ReSharper disable MemberCanBePrivate.Global

namespace Adas.Web.Theme;

/// <summary>
/// Fortescue brand theme for MudBlazor — cloned from the replication-monitor operator
/// shell (aligned to the Figma UI Design System, February 2026) so ADAS reuses the
/// proven branding, palette, typography, and elevation tokens.
///
/// Typography scale mapping (Figma → MudBlazor):
///   H1 = 4xl  (36px / 44px LH)     Subtitle1 = base/medium (16px / 24px LH)
///   H2 = 3xl  (30px / 40px LH)     Subtitle2 = sm/semibold (14px / 20px LH)
///   H3 = 2xl  (24px / 32px LH)     Body1     = base/regular (16px / 24px LH)
///   H4 = xl   (20px / 32px LH)     Body2     = sm/regular   (14px / 20px LH)
///   H5 = lg   (18px / 28px LH)     Caption   = xs/regular   (12px / 16px LH)
///   H6 = base (16px / 24px LH)     Overline  = xs/semibold  (12px / 16px LH)
/// </summary>
public static class FortescueTheme
{
    // ── Brand colour constants (for reuse outside CSS) ──────────────────────
    public const string FortescueBlue = "#001D44";
    public const string FutureGreen   = "#00FF00";
    public const string Cyan          = "#00CDE1";
    public const string MidGreen      = "#0CB94A";
    public const string MidBlue       = "#0F5AD3";
    public const string DarkGreen     = "#003F30";

    // Semantic
    public const string ErrorRed      = "#CC2424";
    public const string SuccessGreen  = "#0A6E32";
    public const string WarningOrange = "#B24C1D";
    public const string InfoBlue      = "#0F5AD3";

    // Secondary
    public const string Yellow = "#FEDC00";
    public const string Orange = "#FF6C2A";
    public const string Pink   = "#F44CBD";
    public const string Purple = "#9F44ED";

    // Chat accent — the operator diagnostic surface signature colour. Named here
    // so C# (MudBlazor Color.Custom callers) and the CSS token stay in step.
    // The CSS layer maps light/dark variants under the .adas-chat theme classes.
    public const string ChatAccent     = "#0F5AD3";
    public const string ChatAccentDark = "#3F7BDC";

    // Neutrals (email templates + C# code that can't use CSS variables)
    public const string White          = "#FFFFFF";
    public const string TextPrimary    = "#0F0F0F";
    public const string TextSecondary  = "#696969";
    public const string BackgroundGrey = "#F4F4F4";
    public const string BorderGrey     = "#C0C0C0";

    // ── Font stacks ─────────────────────────────────────────────────────────
    private static readonly string[] DefaultFonts = ["Roboto", "Helvetica", "Arial", "sans-serif"];

    public static MudTheme Theme => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary              = FortescueBlue,
            PrimaryDarken        = "#001736",
            PrimaryLighten       = "#334A69",

            Secondary            = MidBlue,
            SecondaryDarken      = "#0C48A9",
            SecondaryLighten     = "#3F7BDC",

            Tertiary             = MidGreen,
            Info                 = Cyan,
            Success              = MidGreen,
            Warning              = Yellow,
            Error                = ErrorRed,
            Dark                 = "#1E1E1E",

            AppbarBackground     = FortescueBlue,
            AppbarText           = "#FFFFFF",
            DrawerBackground     = "#FFFFFF",
            DrawerText           = "#0F0F0F",
            Background           = "#FFFFFF",
            BackgroundGray       = "#F4F4F4",
            Surface              = "#FFFFFF",

            TextPrimary          = "#0F0F0F",
            TextSecondary        = "#696969",
            TextDisabled         = "#ABABAB",

            Divider              = "#C0C0C0",
            DividerLight         = "#E5E5E5",
            LinesDefault         = "#C0C0C0",
            LinesInputs          = "#C0C0C0",
            TableLines           = "#E5E5E5",

            ActionDefault                = "#969696",
            ActionDisabled               = "rgba(150, 150, 150, 0.38)",
            ActionDisabledBackground     = "rgba(150, 150, 150, 0.12)",

            HoverOpacity         = 0.04,
            RippleOpacity        = 0.1,

            PrimaryContrastText  = "#FFFFFF",
            SecondaryContrastText = "#FFFFFF",
            TertiaryContrastText = "#FFFFFF",
            InfoContrastText     = "#000000",
            SuccessContrastText  = "#FFFFFF",
            WarningContrastText  = FortescueBlue,
            ErrorContrastText    = "#FFFFFF",
            DarkContrastText     = "#FFFFFF"
        },

        PaletteDark = new PaletteDark
        {
            Primary              = Cyan,
            PrimaryDarken        = "#00A4B4",
            PrimaryLighten       = "#33D7E7",

            Secondary            = MidBlue,
            SecondaryDarken      = "#0C48A9",
            SecondaryLighten     = "#3F7BDC",

            Tertiary             = MidGreen,
            Info                 = Cyan,
            Success              = MidGreen,
            Warning              = Yellow,
            Error                = ErrorRed,
            Dark                 = "#000000",

            Background           = "#121212",
            BackgroundGray       = "#1A1A1A",
            Surface              = "#1E1E1E",
            AppbarBackground     = "#001736",
            AppbarText           = "#FFFFFF",
            DrawerBackground     = "#1E1E1E",
            DrawerText           = "rgba(255, 255, 255, 0.87)",

            TextPrimary          = "rgba(255, 255, 255, 0.87)",
            TextSecondary        = "rgba(255, 255, 255, 0.60)",
            TextDisabled         = "rgba(255, 255, 255, 0.38)",

            Divider              = "rgba(255, 255, 255, 0.12)",
            DividerLight         = "rgba(255, 255, 255, 0.06)",
            LinesDefault         = "rgba(255, 255, 255, 0.12)",
            LinesInputs          = "rgba(255, 255, 255, 0.30)",
            TableLines           = "rgba(255, 255, 255, 0.12)",

            ActionDefault                = "rgba(255, 255, 255, 0.54)",
            ActionDisabled               = "rgba(255, 255, 255, 0.26)",
            ActionDisabledBackground     = "rgba(255, 255, 255, 0.12)",

            HoverOpacity         = 0.08,
            RippleOpacity        = 0.5,

            PrimaryContrastText  = "#121212",
            SecondaryContrastText = "#FFFFFF",
            TertiaryContrastText = "#FFFFFF",
            InfoContrastText     = "#000000",
            SuccessContrastText  = "#FFFFFF",
            WarningContrastText  = FortescueBlue,
            ErrorContrastText    = "#FFFFFF",
            DarkContrastText     = "#FFFFFF"
        },

        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily    = DefaultFonts,
                FontSize      = ".875rem",
                FontWeight    = "400",
                LineHeight    = "1.43",
                LetterSpacing = "normal"
            },
            H1 = new H1Typography
            {
                FontFamily = DefaultFonts, FontSize = "1.875rem", FontWeight = "500", LineHeight = "1.2", LetterSpacing = "0"
            },
            H2 = new H2Typography
            {
                FontFamily = DefaultFonts, FontSize = "1.5rem", FontWeight = "500", LineHeight = "1.333", LetterSpacing = "0"
            },
            H3 = new H3Typography
            {
                FontFamily = DefaultFonts, FontSize = "1.25rem", FontWeight = "500", LineHeight = "1.4", LetterSpacing = "0"
            },
            H4 = new H4Typography
            {
                FontFamily = DefaultFonts, FontSize = "1.125rem", FontWeight = "500", LineHeight = "1.444", LetterSpacing = "0"
            },
            H5 = new H5Typography
            {
                FontFamily = DefaultFonts, FontSize = "1rem", FontWeight = "500", LineHeight = "1.5", LetterSpacing = "0"
            },
            H6 = new H6Typography
            {
                FontFamily = DefaultFonts, FontSize = ".875rem", FontWeight = "600", LineHeight = "1.43", LetterSpacing = "0"
            },
            Subtitle1 = new Subtitle1Typography
            {
                FontFamily = DefaultFonts, FontSize = ".875rem", FontWeight = "500", LineHeight = "1.43", LetterSpacing = "0"
            },
            Subtitle2 = new Subtitle2Typography
            {
                FontFamily = DefaultFonts, FontSize = ".8125rem", FontWeight = "600", LineHeight = "1.38", LetterSpacing = "0"
            },
            Body1 = new Body1Typography
            {
                FontFamily = DefaultFonts, FontSize = ".875rem", FontWeight = "400", LineHeight = "1.43", LetterSpacing = "0"
            },
            Body2 = new Body2Typography
            {
                FontFamily = DefaultFonts, FontSize = ".8125rem", FontWeight = "400", LineHeight = "1.38", LetterSpacing = "0"
            },
            Button = new ButtonTypography
            {
                FontFamily = DefaultFonts, FontSize = ".875rem", FontWeight = "500", LineHeight = "1.43", LetterSpacing = "0.02em", TextTransform = "none"
            },
            Caption = new CaptionTypography
            {
                FontFamily = DefaultFonts, FontSize = ".75rem", FontWeight = "400", LineHeight = "1.333", LetterSpacing = "0"
            },
            Overline = new OverlineTypography
            {
                FontFamily = DefaultFonts, FontSize = ".6875rem", FontWeight = "600", LineHeight = "1.45", LetterSpacing = "0.08em", TextTransform = "uppercase"
            }
        },

        Shadows = new Shadow
        {
            Elevation =
            [
                "none",
                "0px 1px 0px rgba(0,0,0,0.05)",
                "0px 1px 3px rgba(0,0,0,0.1), 0px 1px 2px -1px rgba(0,0,0,0.1)",
                "0px 4px 6px -1px rgba(0,0,0,0.1), 0px 2px 4px -2px rgba(0,0,0,0.1)",
                "0px 4px 6px -1px rgba(0,0,0,0.1), 0px 2px 4px -2px rgba(0,0,0,0.1)",
                "0px 10px 15px -3px rgba(0,0,0,0.1), 0px 4px 6px -4px rgba(0,0,0,0.1)",
                "0px 10px 15px -3px rgba(0,0,0,0.1), 0px 4px 6px -4px rgba(0,0,0,0.1)",
                "0px 10px 15px -3px rgba(0,0,0,0.1), 0px 4px 6px -4px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 20px 25px -5px rgba(0,0,0,0.1), 0px 8px 10px -6px rgba(0,0,0,0.1)",
                "0px 0px 50px rgba(0,255,0,0.3)"
            ]
        },

        ZIndex = new ZIndex
        {
            Drawer   = 1200,
            AppBar   = 1100,
            Dialog   = 1300,
            Popover  = 1400,
            Snackbar = 1400,
            Tooltip  = 1500
        }
    };
}
