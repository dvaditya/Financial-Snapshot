import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "../_services/auth.service";

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthService
        ) {}
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.authService.currentUserValue;
        if(user){
            return true;
        }

        // Redirect to login with return url so that user can be redirected to the page he originally intended.
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}