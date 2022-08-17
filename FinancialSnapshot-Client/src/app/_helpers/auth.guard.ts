import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "../_services/auth.service";

export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthService
        ) {}
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const user = this.authService.currentUser;
        return false;
    }
}