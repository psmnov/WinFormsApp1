using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1;

public static class HandleAdminChanges
{
 
    public static void dontPressTheOk(object sender, MouseEventArgs e, Button btnOK, Size ClientSize)
    {

        int newX = btnOK.Location.X;
        int newY = btnOK.Location.Y;

        // Движение по оси X
        if (e.X < btnOK.Location.X && btnOK.Location.X > 1)
            newX += 1;
        else if (e.X > btnOK.Location.X + btnOK.Width && btnOK.Location.X < ClientSize.Width - btnOK.Width)
            newX -= 1;

        // Движение по оси Y
        if (e.Y < btnOK.Location.Y && btnOK.Location.Y > 1)
            newY += 1;
        else if (e.Y > btnOK.Location.Y + btnOK.Height && btnOK.Location.Y < ClientSize.Height - btnOK.Height)
            newY -= 1;

        // Проверка границ
        if (newX < 0) newX = 0;
        if (newX > ClientSize.Width - btnOK.Width) newX = ClientSize.Width - btnOK.Width;
        if (newY < 0) newY = 0;
        if (newY > ClientSize.Height - btnOK.Height) newY = ClientSize.Height - btnOK.Height;

        if (IsButtonInCorner(btnOK, ClientSize))
        {
            Random r = new Random();
            newY = r.Next(1, btnOK.Height);
            newX = r.Next(1, btnOK.Width);
        }
        btnOK.Location = new Point(newX, newY);
        btnOK.BringToFront();
    }
    private static bool IsButtonInCorner(Button button, Size ClientSize)
    {
        if ((button.Location.X == 0 && button.Location.Y == 0) ||
            (button.Location.X + button.Width == ClientSize.Width && button.Location.Y == 0) ||
            (button.Location.X == 0 && button.Location.Y + button.Height == ClientSize.Height) ||
            (button.Location.X + button.Width == ClientSize.Width && button.Location.Y + button.Height == ClientSize.Height))
            return true;

        return false;
    }
}
