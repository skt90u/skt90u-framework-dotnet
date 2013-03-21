using System;

public static class HumanResources
{
    public static int GetEmployeeCount(string department)
    {
        int count = 0;
        switch (department)
        {
            case "Sales":
                count = 10;
                break;

            case "Engineering":
                count = 28;
                break;

            case "Marketing":
                count = 44;
                break;

            case "HR":
                count = 7;
                break;

            default:
                break;
        }

        return count;
    }
}

