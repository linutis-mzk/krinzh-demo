
import { useNavigate, NavigateFunction } from "react-router-dom";


export class RoutesHelper {
   private static route: string = "/";
   private static navigateFunc : NavigateFunction; 
   
   constructor()
   {
      RoutesHelper.navigateFunc = useNavigate();
      RoutesHelper.navigateFunc(RoutesHelper.route);
   }

   public static Refresh()
   {
      const nav = useNavigate();
      nav(-1);
   }
   public static Navigate(route: string) {
      RoutesHelper.route = route;
      RoutesHelper.navigateFunc(route);
   }
}